using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using ProfileService.Core.Repositories;
using ProfileService.Core.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Configure EasyNetQ IBus
var rabbitMqConnectionString = "host=rabbitmq;port=5672;username=guest;password=guest";
builder.Services.AddSingleton<IBus>(RabbitHutch.CreateBus(rabbitMqConnectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProfileServiceContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("YourConnectionStringName")));
builder.Services.AddScoped<ProfileServiceRepository>();
builder.Services.AddScoped<ProfileService.Core.Services.ProfilService>();

var app = builder.Build();

var bus = app.Services.GetRequiredService<IBus>();
var profileSubscriberService = new Subscriber(bus);
bool subscribed = false;
int retryCount = 0;
while (!subscribed && retryCount < 5) {
    try {
        profileSubscriberService.Subscribe("profile");
        subscribed = true;
    } catch (Exception ex) {
        retryCount++;
        Console.WriteLine("ERROR SUBSCRIBING TO PROFILE: " + ex.Message);
        Task.Delay(1000);
    }
}
//profileSubscriberService.Subscribe("profile");

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();