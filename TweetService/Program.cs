using Microsoft.EntityFrameworkCore;
using TweetService.Core.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TweeServiceContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("YourConnectionStringName")));
builder.Services.AddScoped<TweetServiceRepository>();
builder.Services.AddScoped<TweetService.Core.Services.TweetService>();
var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();