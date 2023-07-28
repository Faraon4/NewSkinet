using API.Errors;
using API.Extensions;
using API.Middleware;
using Core.Entities.Identity;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation(); // This is a new method that we added

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>(); // middleware : to show the server error correct

app.UseStatusCodePagesWithReExecute("/errors/{0}"); //  we create a new controller, and here we put the route to there such whay when we hit a unexisting point we will have correct response


if (app.Environment.IsDevelopment())
{
    app.UserSwaggerDocumentation();
}

// We put this command here to tell the program to use static file , in our case are the images
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var identityContext = services.GetRequiredService<AppIdentityDbContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
await context.Database.MigrateAsync();
await identityContext.Database.MigrateAsync();
await StoreContextSeed.SeedAsync(context);
// Take the method that we created in prev commit with info about one user's information
await AppIdentityDbContextSeed.SeedUserAsync(userManager);
}
catch(Exception e)
{
logger.LogError(e, "An error occured during migration");
}

app.Run();
