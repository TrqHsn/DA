using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class DateTimeExtension
    {
        public static int CalculateAge(this DateTime dob)
        {
            var today = DateTime.Today;

            var age = today.Year - dob.Year;

            if (dob.Date > today.AddYears(-age)) age--;

            return age;
        }
        // public static int CalculateAges(this DateTime dob)
        // {
        //     DateTime n = DateTime.Now;
        //     int age = n.Year - dob.Year;

        //     if (n.Month < dob.Month || (n.Month == dob.Month && n.Day < dob.Day)) age--;
            
        //     return age;
        // }
   

    }
}