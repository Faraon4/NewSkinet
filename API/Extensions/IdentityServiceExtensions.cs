using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<AppIdentityDbContext>(opt => {
                opt.UseSqlite(configuration.GetConnectionString("IdentityConnection"));
            });
            services.AddIdentityCore<AppUser> (opt => 
            {
                // we can add identity options here
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddSignInManager<SignInManager<AppUser>>();

            // It is important to add Authentication and Authorization in this order, because on ly like this we check who the pers is and then what he/she can do
            services.AddAuthentication();
            services.AddAuthorization();

            return services;
        }
    }
}