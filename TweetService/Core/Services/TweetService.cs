using Domain;
using Domain.DTO;
using Domain.Helpers;
using EasyNetQ;
using Newtonsoft.Json;
using TweetService.Core.Repositories;

namespace TweetService.Core.Services;

public class TweetService
{
    private readonly TweetServiceRepository _repository;
    
    public TweetService(TweetServiceRepository repository)
    {
        _repository = repository;
    }


    public Tweet AddTweet(PostTweetDTO dto)
    {
        //Map
        var tweet = new Tweet()
        {
            Text = dto.Text,
            CreatedAt = dto.CreatedAt,
            UserId = dto.UserId
        };
        
        Tweet addedTweet = _repository.AddTweet(tweet);
        
        //Publish Tweet for Profile Service
        var connectionString = Environment.GetEnvironmentVariable("EASYNETQ_CONNECTION_STRING");
        var bus = RabbitHutch.CreateBus(connectionString);
        var publisher = new Publisher(bus);
        var subscriber = new Subscriber(bus);

       //subscriber.Subscribe("profile");
       // Console.WriteLine("Subscribed to tweet");
      //  Thread.Sleep(5000);
       // publisher.PublishMessageAsync("New tweet", "tweet");
       // Thread.Sleep(2000);
      
       var tweetMessage = new TweetMessage
       {
           MessageType = "Tweet",
           UserId = tweet.UserId,
           Text = tweet.Text,
           CreatedAt = tweet.CreatedAt
       };
       publisher.PublishMessageAsync(tweetMessage, "profile");
       Console.WriteLine("Tweet published");
       return addedTweet;
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