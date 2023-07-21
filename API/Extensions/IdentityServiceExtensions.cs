using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => 
                    {
                        // We are writing what we want to do to validate this token
                        options.TokenValidationParameters = new TokenValidationParameters // this TokenValidationParameters -> is very important from where to import
                        {
                            // This is first what it is needed to be validated
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                            ValidIssuer = configuration["Token:Issuer"],
                            ValidateIssuer = true

                        };
                    });



            services.AddAuthorization();

            return services;
        }
    }
}