using Domain;

namespace TweetService.Core.Repositories;

public class TweetServiceRepository
{
    private TweeServiceContext _context;
    public TweetServiceRepository(TweeServiceContext context)
    {
        _context = context;
    }
    
    public void AddTweet(Tweet tweet)
    {
        _context.TweetTable.Add(tweet);
        _context.SaveChanges();
    }

    public void RebuildDB()
    {
        _context.Database.EnsureDeleted();
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
}