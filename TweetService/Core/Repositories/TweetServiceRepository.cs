using Domain;

namespace TweetService.Core.Repositories;

public class TweetServiceRepository : ITweetRepository
{
    private TweeServiceContext _context;
    public TweetServiceRepository(TweeServiceContext context)
    {
        _context = context;
        RebuildDB();
    }
    
    public TweetServiceRepository()
    {
        
    }
    
    public Tweet AddTweet(Tweet tweet)
    {
        _context.TweetTable.Add(tweet);
        _context.SaveChanges();
        return tweet;
    }

    public void RebuildDB()
    {
        _context.Database.EnsureCreated();
    }

    public void DeleteTweet(int tweetId)
    {
        _context.TweetTable.Remove(_context.TweetTable.Find(tweetId));

        _context.SaveChanges();

    }

    public List<Tweet> GetTweets(int userId)
    {
        return _context.TweetTable.Where(t => t.UserId == userId).ToList();
    }

    public Tweet GetTweet(int tweetId)
    {
        return _context.TweetTable.Find(tweetId) ?? throw new KeyNotFoundException("Tweet not found");
    }
}