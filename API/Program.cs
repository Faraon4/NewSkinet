using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StoreContext>(opt => 
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")); // Add the service of the connection to the db that we write in the appsetting.Dev.json
});

builder.Services.AddScoped<IProductRepos, ProductRepository>(); // We can create AddTransient , or AddSingelton -> but AddScoped is better way and simpler
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // We used the typeof and <>empty , because we have T there , where T can be of typeBaseEntity


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// We put this command here to tell the program to use static file , in our case are the images
app.UseStaticFiles();

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
