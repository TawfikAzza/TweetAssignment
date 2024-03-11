using EasyNetQ;

namespace Domain.Helpers;

public class Publisher
{
    private readonly IBus bus;

    public Publisher(IBus bus)
    {
        this.bus = bus;
    }

    public async Task PublishMessageAsync(ITMessage message, string topic)
    {
        await bus.PubSub.PublishAsync(message, topic);
    }
}