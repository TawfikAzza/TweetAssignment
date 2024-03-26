using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain;
using Domain.DTO;
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
    public async Task<ActionResult<Tweet>> AddTweet(PostTweetDTO dto)
    {
        var response = _tweetApi.PostAsync("TweetService/Tweet", new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json"));
        Console.WriteLine(response.Result.StatusCode);
    
        if (!response.Result.IsSuccessStatusCode)
        {
            return StatusCode(500);
        }

        var responseContent = response.Result.Content.ReadAsStringAsync();

        // Deserialize response content to Tweet object
        var tweet = JsonSerializer.Deserialize<Tweet>(responseContent.Result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (tweet == null)
        {
            return StatusCode(500, "Failed to deserialize response from the server.");
        }

        // Return the deserialized tweet
        return tweet;
    }

    
    
    [HttpDelete("Tweet")]
    public ActionResult DeleteTweet(int tweetId)
    {
        var response = _tweetApi.DeleteAsync("TweetService/Tweet?tweetId=" + tweetId);
        return StatusCode(response.Result.StatusCode.GetHashCode());
    }
    
    [HttpGet("Tweet")]
    public ActionResult<List<Tweet>> GetTweets(int userId)
    {
        var response = _tweetApi.GetAsync("TweetService/Tweet?userId=" + userId);
        if (response.Result.IsSuccessStatusCode)
        {
            var responseContent = response.Result.Content.ReadAsStringAsync();
            var tweets = JsonSerializer.Deserialize<List<Tweet>>(responseContent.Result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (tweets == null)
            {
                return StatusCode(500, "Failed to deserialize response from the server.");
            }
            return tweets;
        }
        return StatusCode(response.Result.StatusCode.GetHashCode());
    }
}