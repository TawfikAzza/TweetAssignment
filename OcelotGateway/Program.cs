using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile("ocelot.json", false, false);


// Add services to the container.
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseOcelot().Wait();

app.UseAuthorization();

app.MapControllers();

app.Run();