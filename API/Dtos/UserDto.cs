using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Displayname { get; set; } // this is what we'll display on nav bar in our angular application
        public string Token { get; set; }
    }
}