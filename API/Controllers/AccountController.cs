using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //y[Authorize]
    public class AccountController : ControllerBase
    {
        public DataContext _context;
        public ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]


        // public async Task<ActionResult<UserDto>> Registers(RegisterDto rd)
        // {
        //     if (await UE(rd.UserName)) return Unauthorized("Username already taken");
        //     using var hmacc = new HMACSHA512();
        //     var user = new AppUser
        //     {
        //         UserName = rd.UserName,
        //         PasswordHash = hmacc.ComputeHash(Encoding.UTF8.GetBytes(rd.Password)),
        //         PasswordSalt = hmacc.Key
        //     };
        //     _context.Users.Add(user);
        //     await _context.SaveChangesAsync();
        //     return new UserDto
        //     {
        //         UserName = user.UserName,
        //         Token = _tokenService.CreateToken(user)
        //     };
        // }
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExist(registerDto.UserName)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
 
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                UserName =user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]

        // public async Task<ActionResult<UserDto>> Logn(LoginDto ld)
        // {
        //     var user = await _context.Users.SingleOrDefaultAsync(x=> x.UserName==rd.UserName.ToLower());
        //     if (user == null) return Unauthorized ("user doesent exist");
        //     using var hmac =new HMACSHA512(Encoding.UTF8.GetBytes(user.PasswordSalt));
        //     var computHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(ld.Password));
        //     for (int i=0; i< computHash.Length; i++)
        //     {
        //         if(computHash[i]!=user.PasswordHash[i]) return Unauthorized("pswrd ddnt match");
        //     }
        //     return new UserDto
        //     {
        //         UserName = user.UserName,
        //         Token = _tokenService.CreateToken(user)
        //     };
        // }
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x=> x.UserName == loginDto.UserName.ToLower());
            if (user == null) return Unauthorized("Invalid username");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i =0; i<computedHash.Length;i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }
            return new UserDto
            {
                UserName =user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserExist(string username)
        {
            return await _context.Users.AnyAsync(x=>x.UserName == username.ToLower());   
        }

        private async Task<bool> UE(string usrnm)
        {
            return await _context.Users.AnyAsync(x=>x.UserName == usrnm.ToLower());
        }

    }
}