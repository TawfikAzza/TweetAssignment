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

    public TweetService()
    {
        
    }
    public virtual Tweet AddTweet(PostTweetDTO dto)
    {
        var tweet = new Tweet()
        {
            Text = dto.Text,
            CreatedAt = dto.CreatedAt,
            UserId = dto.UserId
        };

        Tweet addedTweet = _repository.AddTweet(tweet);

        var publisher = new Publisher();

        var tweetMessage = new TweetMessage
        {
            MessageType = "Tweet",
            UserId = addedTweet.UserId,
            Text = addedTweet.Text,
            CreatedAt = addedTweet.CreatedAt,
            Id = addedTweet.Id
        };
        publisher.PublishMessageAsync(tweetMessage, "tweet.add");
        return addedTweet;
    }

    public void RebuildDB()
    {
        _repository.RebuildDB();
    }

    public void DeleteTweet(int tweetId)
    {
        var tweet = _repository.GetTweet(tweetId);
        _repository.DeleteTweet(tweetId);
        Publisher publisher = new Publisher();
        if(tweet == null) throw new KeyNotFoundException("Tweet not found");
        var tweetMessage = new TweetMessage
        {
            MessageType = "Tweet",
            UserId = tweet.UserId,
            Text = tweet.Text,
            CreatedAt = tweet.CreatedAt
        };
        publisher.PublishMessageAsync(tweetMessage, "tweet.delete");
    }

    public List<Tweet> GetTweets(int userId)
    {
        return _repository.GetTweets(userId);
    }
}