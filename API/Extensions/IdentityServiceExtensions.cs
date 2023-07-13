using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Identity;
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

            return services;
        }
    }
}