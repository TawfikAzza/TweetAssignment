using Domain.Helpers;
using EasyNetQ;

namespace ProfileService.Core.Helpers;

public class Subscriber
{
    private readonly IBus bus;

    public Subscriber(IBus bus)
    {
        this.bus = bus;
    }

    public async Task Subscribe(string topic)
    {
        
        //bus.PubSub.Subscribe<TMessage>("1", HandleMessage, x => x.WithTopic(topic));
        await bus.PubSub.SubscribeAsync<TMessage>(topic, HandleMessage, x => x.WithTopic(topic));
        Console.WriteLine($"Subscribed to {topic}");
    }

    private void HandleMessage(TMessage message)
    {
        Console.WriteLine($"Received In Profile:  {message.Text}");
    }
}