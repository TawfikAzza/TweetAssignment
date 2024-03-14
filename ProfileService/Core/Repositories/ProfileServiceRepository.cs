using Domain;
using Microsoft.EntityFrameworkCore;

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
        if (profile.Tweets != null && profile.Tweets.Count >= MAX_TWEETS)
        {
            var oldestTweet = profile.Tweets.OrderBy(t => t.CreatedAt).FirstOrDefault();
            if (oldestTweet != null)
            {
                profile.Tweets.Remove(oldestTweet);
            }
        }

        Console.WriteLine("Adding tweet to profile");
        profile.Tweets.Add(tweet);
        Console.WriteLine("Updating profile");
        Profile profileToUpdate = new Profile()
        {
            Bio = profile.Bio,
            Id = profile.Id,
            UserId = profile.UserId,
            Username = profile.Username,
            Tweets = profile.Tweets
        };
        // _context.SaveChanges();
        UpdateProfile(profileToUpdate);
    }
    public void DeleteTweetFromProfile(Profile profileSent, Tweet tweetToDelete)
    {
        Profile profile = _context.ProfileTable.Include(p => p.Tweets).FirstOrDefault(p => p.Id == profileSent.Id) ?? throw new KeyNotFoundException("Profile not found");
        if (profile.Tweets.Contains(tweetToDelete))
        {
            profile.Tweets.Remove(tweetToDelete);
            _context.ProfileTable.Update(profile);
            _context.SaveChanges();
        }
        
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