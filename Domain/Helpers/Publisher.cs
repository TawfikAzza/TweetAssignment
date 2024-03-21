using EasyNetQ;

namespace Domain.Helpers;

public class Publisher
{
    private readonly IBus _bus;
    public Publisher()
    {
        var connectionString = Environment.GetEnvironmentVariable("EASYNETQ_CONNECTION_STRING"); 
        _bus = RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest");
    }

    public async Task PublishMessageAsync(ITMessage message, string topic)
    {
        Console.WriteLine("Publishing in topic: " + topic);
        await _bus.PubSub.PublishAsync(message, topic);
    }
}