using Domain;
using Microsoft.AspNetCore.Mvc;

namespace TweetService.Controllers;

[ApiController]
[Route("[controller]")]
public class TweetServiceController : ControllerBase
{
    private readonly Core.Services.TweetService _service;
    public TweetServiceController(Core.Services.TweetService service)
    {
        _service = service;
    }
    [HttpGet("rebuildDB")]
    public void RebuildDB()
    {
        _service.RebuildDB();
    }

    [HttpPost("Tweet")]
    public void AddTweet(Tweet tweet)
    {
        _service.AddTweet(tweet);
    }
    
    [HttpDelete("Tweet")]
    public void DeleteTweet(int tweetId)
    {
        _service.DeleteTweet(tweetId);
    }
    
    [HttpGet("Tweet")]
    public List<Tweet> GetTweets(int userId)
    {
        return _service.GetTweets(userId);
    }

}