using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
        {       
            //Old version
            // return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            // New version
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}