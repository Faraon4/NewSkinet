using API.Errors;
using API.Extensions;
using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>(); // middleware : to show the server error correct

app.UseStatusCodePagesWithReExecute("/errors/{0}"); //  we create a new controller, and here we put the route to there such whay when we hit a unexisting point we will have correct response


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// We put this command here to tell the program to use static file , in our case are the images
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();


using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
await context.Database.MigrateAsync();
await StoreContextSeed.SeedAsync(context);
}
catch(Exception e)
{
logger.LogError(e, "An error occured during migration");
}

app.Run();
