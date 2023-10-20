using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {

            
            services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection")); // Add the service of the connection to the db that we write in the appsetting.Dev.json
            });
            services.AddSingleton<IConnectionMultiplexer>(c =>  // IconnectionMultiplexer -> it will be used by API to connect to RedisDb
            {
                var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
                options.AbortOnConnectFail = false;
                
                return ConnectionMultiplexer.Connect(options); // We return ConnectionMultiplexer, but at the beginning we are calling Interface with the same name
            }); 

            services.AddScoped<IBasketRepository, BasketRepository>();

            services.AddScoped<IProductRepos, ProductRepository>(); // We can create AddTransient , or AddSingelton -> but AddScoped is better way and simpler
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // We used the typeof and <>empty , because we have T there , where T can be of typeBaseEntity
            services.AddScoped<ITokenService, TokenService>();


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // We use the next Service for validation error, so when we have 400 error and there are more than one error , we are using this , in form of array
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                 .Where(e => e.Value.Errors.Count > 0)
                                 .SelectMany(x => x.Value.Errors)
                                 .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });


            // Cors -> we are allowing the browser to know from where the data is comming and where is going the https that we put here , ithe destination
            // what to allow is also needed to be written here
            // in this case is the header and the methods
            // Name of the policy that we use is "CorsPolicy", and we add it as well in the Programclass
            services.AddCors(opt => 
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

            return services;
        }
    }
}