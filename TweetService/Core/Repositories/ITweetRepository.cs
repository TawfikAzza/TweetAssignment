using Domain;

namespace TweetService.Core.Repositories;

public interface ITweetRepository
{


    public Tweet AddTweet(Tweet tweet);


    public void RebuildDB();

    public void DeleteTweet(int tweetId);

    public List<Tweet> GetTweets(int userId);

    public Tweet GetTweet(int tweetId);
}