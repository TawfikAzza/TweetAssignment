using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using ProfileService.Core.Repositories;
using ProfileService.Core.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Configure EasyNetQ IBus
var rabbitMqConnectionString = Environment.GetEnvironmentVariable("EASYNETQ_CONNECTION_STRING");
//var rabbitMqConnectionString = "host=rabbitmq;port=5672;username=guest;password=guest";
builder.Services.AddSingleton<IBus>(RabbitHutch.CreateBus(rabbitMqConnectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProfileServiceContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("YourConnectionStringName")));
builder.Services.AddScoped<ProfileServiceRepository>();
builder.Services.AddScoped<ProfileService.Core.Services.ProfileService>();
builder.Services.AddSingleton<IBus>(RabbitHutch.CreateBus(rabbitMqConnectionString));
// Add Subscriber to the DI container
builder.Services.AddScoped<Subscriber>();

var app = builder.Build();
// Use DI to resolve Subscriber and automatically subscribe
var scope = app.Services.CreateScope();
var subscriberService = scope.ServiceProvider.GetRequiredService<Subscriber>();

bool subscribed = false;
int retryCount = 0;
while (!subscribed && retryCount < 5) {
    try {
        await subscriberService.Subscribe("profile");
        await subscriberService.SubscribeToAddUser();
        await subscriberService.SubscribeToEditUser();
        await subscriberService.SubscribeToDeleteTweet();
        await subscriberService.SubscribeToAddTweet();
        subscribed = true;
    } catch (Exception ex) {
        retryCount++;
        Console.WriteLine("ERROR SUBSCRIBING TO PROFILE: " + ex.Message);
        await Task.Delay(1000);
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