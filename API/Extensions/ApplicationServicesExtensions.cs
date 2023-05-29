using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection")); // Add the service of the connection to the db that we write in the appsetting.Dev.json
            });

            services.AddScoped<IProductRepos, ProductRepository>(); // We can create AddTransient , or AddSingelton -> but AddScoped is better way and simpler
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // We used the typeof and <>empty , because we have T there , where T can be of typeBaseEntity


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
            return services;
        }
    }
}