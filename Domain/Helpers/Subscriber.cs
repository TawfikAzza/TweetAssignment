using EasyNetQ;

namespace Domain.Helpers;

public class Subscriber
{
    private readonly IBus _bus;

    public Subscriber()
    {
        var connectionString = Environment.GetEnvironmentVariable("EASYNETQ_CONNECTION_STRING"); 
        _bus = RabbitHutch.CreateBus(connectionString);
    }

    public async Task Subscribe(string topic)
    {
        
        //bus.PubSub.Subscribe<TMessage>("1", HandleMessage, x => x.WithTopic(topic));
        await _bus.PubSub.SubscribeAsync<ITMessage>(topic, HandleMessage, x => x.WithTopic(topic));
        
    }

    private void HandleMessage(ITMessage message)
    {
        Console.WriteLine($"Received: {message.MessageType}");
    }
}