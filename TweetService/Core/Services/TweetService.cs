using Domain;
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