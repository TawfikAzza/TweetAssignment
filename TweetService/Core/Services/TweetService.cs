using Domain;
using Domain.Helpers;
using EasyNetQ;
using TweetService.Core.Repositories;

namespace TweetService.Core.Services;

public class TweetService
{
    private readonly TweetServiceRepository _repository;
    
    public TweetService(TweetServiceRepository repository)
    {
        _repository = repository;
    }


    public void AddTweet(Tweet tweet)
    {
        _repository.AddTweet(tweet);
        var connectionString = Environment.GetEnvironmentVariable("EASYNETQ_CONNECTION_STRING");
        var bus = RabbitHutch.CreateBus(connectionString);
        var publisher = new Publisher(bus);
        var subscriber = new Subscriber(bus);
       subscriber.Subscribe("profile");
       // Console.WriteLine("Subscribed to tweet");
        Thread.Sleep(5000);
       // publisher.PublishMessageAsync("New tweet", "tweet");
       // Thread.Sleep(2000);
        publisher.PublishMessageAsync("New tweet", "profile");
        Console.WriteLine("Tweet published");
    }

    public void RebuildDB()
    {
       _repository.RebuildDB();
    }

    public void DeleteTweet(int tweetId)
    {
        _repository.DeleteTweet(tweetId);
    }

    public List<Tweet> GetTweets(int userId)
    {
        return _repository.GetTweets(userId);
    }
}