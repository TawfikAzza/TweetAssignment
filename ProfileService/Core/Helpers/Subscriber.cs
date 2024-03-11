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
        await bus.PubSub.SubscribeAsync<ITMessage>(topic, HandleMessage, x => x.WithTopic(topic));
        Console.WriteLine($"Subscribed to {topic}");
    }

    private void HandleMessage(ITMessage message)
    {
        Console.WriteLine($"Received In Profile:  {message.MessageType}");
        switch (message.MessageType)
        {
            case "User":
                var userMessage = message as UserMessage;
                // Handle user message
                Console.WriteLine($"Received user update: {userMessage.Username}");
                break;
            case "Tweet":
                var tweetMessage = message as TweetMessage;
                // Handle tweet message
                Console.WriteLine($"Received tweet: {tweetMessage.Text}");
                break;
            default:
                Console.WriteLine("Unknown message type.");
                break;
        }
    }
}