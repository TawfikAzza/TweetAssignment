using EasyNetQ;

namespace Domain.Helpers;

public class Publisher
{
    private readonly IBus bus;

    public Publisher(IBus bus)
    {
        this.bus = bus;
    }

    public async Task PublishMessageAsync(string messageText, string topic)
    {
        var message = new TMessage { Text = messageText };
        await bus.PubSub.PublishAsync(message, topic);
    }
}