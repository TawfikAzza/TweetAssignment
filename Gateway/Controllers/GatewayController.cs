using System.Text;
using System.Text.Json;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace TweetAssignment.Controllers;

[ApiController]
[Route("[controller]")]
public class GatewayController : ControllerBase
{
    private HttpClient _profileApi = new() { BaseAddress = new Uri("http://profile") };
    private HttpClient _userApi = new() { BaseAddress = new Uri("http://user") };
    private HttpClient _tweetApi = new() { BaseAddress = new Uri("http://tweet") };
    
    public GatewayController() {
        //TODO: Define where to send the requests
    }
    
    // Profile
    [HttpGet("Profile")]
    public Profile GetProfile(int userId)
    {
        return new Profile();
    }
    
    // Users
    [HttpPost("User")]
    public void AddUser(User user)
    {
        //TODO: Send the request to the UserService
    }
    
    [HttpDelete("User")]
    public void DeleteUser(int userId)
    {
        //TODO: Send the request to the UserService 
    }
    
    [HttpGet("User")]
    public User GetUser(int userId)
    {
        return new User();
    }
    
    // Tweets
    [HttpPost("Tweet")]
    public void AddTweet(Tweet tweet) //TODO: Return action result, also PostTweetDTO (w/o id)
    {
        _tweetApi.PostAsync("TweetServiceController/Tweet", new StringContent(JsonSerializer.Serialize(tweet), Encoding.UTF8, "application/json"));
    }
    
    [HttpDelete("Tweet")]
    public void DeleteTweet(int tweetId)
    {
        _tweetApi.DeleteAsync("TweetServiceController/Tweet?tweetId=" + tweetId);
    }
    
    [HttpGet("Tweet")]
    public List<Tweet> GetTweets(int userId)
    {
        var response = _tweetApi.GetAsync("TweetServiceController/Tweet?userId=" + userId);
        if (response.Result.IsSuccessStatusCode)
        {
            var responseContent = response.Result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Tweet>>(responseContent.Result);
        }
        return new List<Tweet>();
    }
}