using Domain;
using Microsoft.EntityFrameworkCore;

using ProfileService.Core.Helpers;


namespace ProfileService.Core.Repositories;

public class ProfileServiceRepository
{
    private readonly ProfileServiceContext _context;
    private readonly int MAX_TWEETS = 10;
    public ProfileServiceRepository(ProfileServiceContext context)
    {
        _context = context;
        rebuildDB();
    }

    public Profile GetProfile(int userId)
    {
        return _context.ProfileTable.Include(p=> p.Tweets).FirstOrDefault(p => p.UserId == userId) ?? throw new KeyNotFoundException("Profile not found");
    }

    // public void AddOrUpdateTweetToProfile(Profile profileSent, Tweet tweet)
    // {
    //     Profile profile = _context.ProfileTable.Find(profileSent.Id) ?? throw new KeyNotFoundException("Profile not found");
    //     Console.WriteLine("Checking if tweet limit is reached");
    //     if (profile.Tweets.Count >= MAX_TWEETS)
    //     {
    //         var oldestTweet = profile.Tweets.OrderBy(t => t.CreatedAt).First();
    //         profile.Tweets.Remove(oldestTweet);
    //     }
    //
    //     Console.WriteLine("Adding tweet to profile");
    //     profile.Tweets.Add(tweet);
    //    // _context.ProfileTable.Update(profile);
    //     _context.SaveChanges();
    // }

    public void AddOrUpdateTweetToProfile(Profile profileSent, Tweet tweet)
    {
        var profile = _context.ProfileTable
            .Include(p => p.Tweets)
            .FirstOrDefault(p => p.Id == profileSent.Id) ?? throw new KeyNotFoundException("Profile not found");

        Console.WriteLine("Checking if tweet limit is reached");


        if (profile.Tweets.Count >= MAX_TWEETS)
        {
            var oldestTweet = profile.Tweets.OrderBy(t => t.CreatedAt).FirstOrDefault();
            
            if (oldestTweet != null)
            {
                _context.ProfileTweetTable.Remove(oldestTweet); 

            }
        }

        Console.WriteLine("Adding tweet to profile");

        profile.Tweets.Add(new ProfileTweet
        {
            Text = tweet.Text,
            CreatedAt = tweet.CreatedAt,
            UserId = tweet.UserId,
            ProfileId = profile.Id,
            TweetId = tweet.Id
        });

        Console.WriteLine("Updating profile");


        _context.SaveChanges();

    }
    public void DeleteTweetFromProfile(Profile profileSent, Tweet tweetToDelete)
    {
        Profile profile = _context.ProfileTable.Include(p => p.Tweets).FirstOrDefault(p => p.Id == profileSent.Id) ?? throw new KeyNotFoundException("Profile not found");
        Console.WriteLine("Deleting tweet from profile");
        ProfileTweet tweet = _context.ProfileTweetTable.FirstOrDefault(t => t.TweetId == tweetToDelete.Id) ?? throw new KeyNotFoundException("Tweet not found");
        Console.WriteLine("Tweet found" + tweet.Text);
        _context.ProfileTweetTable.Remove(tweet);
        if (profile.Tweets.Contains(tweet))
        {
            profile.Tweets.Remove(tweet);
        
            _context.ProfileTable.Update(profile);
           
        }
        _context.SaveChanges();
        
    }
    public void rebuildDB()
    {
        _context.Database.EnsureCreated();
    }

    public Profile UpdateProfile(Profile profile)
    {
        Console.WriteLine("Tweets Size: " + profile.Tweets.Count);
        _context.ProfileTable.Update(profile);
        _context.SaveChanges();
        return profile;
    }

    public void AddProfile(Profile profile)
    {
        _context.ProfileTable.Add(profile);
        _context.SaveChanges();
        Console.WriteLine($"Profile added for user : {profile.UserId} with username: {profile.Username}");
    }

   
}