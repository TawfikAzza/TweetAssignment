using EasyNetQ;

namespace Domain.Helpers;

public class Publisher
{
    private readonly IBus _bus;
    public Publisher(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new System.ArgumentException("IsNullOrEmpty", nameof(connectionString));
        }
        _bus = RabbitHutch.CreateBus(connectionString);
    }

    public Publisher()
    {
        
    } // For testing purposes

    public virtual async Task PublishMessageAsync(ITMessage message, string topic)
    {
        
        await _bus.PubSub.PublishAsync(message, topic);
    }
}