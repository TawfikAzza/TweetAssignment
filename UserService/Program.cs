using Microsoft.EntityFrameworkCore;
using UserService.Core.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserServiceContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("YourConnectionStringName")));
builder.Services.AddScoped<UserServiceRepository>();
builder.Services.AddScoped<UserService.Core.Services.UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();