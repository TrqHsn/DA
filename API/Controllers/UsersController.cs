using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _iur;
        private readonly IMapper _mapper;
        
        public UsersController(IUserRepository iur, IMapper mapper)
        {
            _mapper = mapper;
            _iur = iur;
        }

        [HttpGet ]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _iur.GetMembersAsync();

            return Ok(users);
        }

        [HttpGet ("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _iur.GetMemberAsync(username);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _iur.GetUserByUsernameAsync(username);

            _mapper.Map(memberUpdateDto, user);

            _iur.Updat(user);

            if(await _iur.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to update user");
        }

    }
}