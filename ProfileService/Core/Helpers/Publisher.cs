using Domain.Helpers;
using EasyNetQ;

namespace ProfileService.Core.Helpers;

public class Publisher
{
    private readonly IBus bus;
    private readonly IBus _bus;
    public Publisher()
    {
        var connectionString = Environment.GetEnvironmentVariable("EASYNETQ_CONNECTION_STRING"); 
        _bus = RabbitHutch.CreateBus(connectionString);
        
    }

    public async Task PublishMessageAsync(string messageText, string topic)
    {
        var message = new TMessage { Text = messageText };
        await _bus.PubSub.PublishAsync(message, topic);
    }
}