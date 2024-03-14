using EasyNetQ;

namespace Domain.Helpers;

public class Publisher
{
    private readonly IBus _bus;
    public Publisher()
    {
        var connectionString = Environment.GetEnvironmentVariable("EASYNETQ_CONNECTION_STRING"); 
        _bus = RabbitHutch.CreateBus(connectionString);
    }

    public async Task PublishMessageAsync(ITMessage message, string topic)
    {
        await _bus.PubSub.PublishAsync(message, topic);
    }
}