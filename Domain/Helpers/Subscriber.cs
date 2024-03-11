﻿using EasyNetQ;

namespace Domain.Helpers;

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
        
    }

    private void HandleMessage(ITMessage message)
    {
        Console.WriteLine($"Received: {message.MessageType}");
    }
}