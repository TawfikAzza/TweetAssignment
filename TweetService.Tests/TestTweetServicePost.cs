using Domain;
using Domain.DTO;
using Moq;
using TweetService.Controllers;
using TweetService.Core.Repositories;

namespace TweetService.Tests;

public class TestTweetServicePost
{
    [Fact]
    public void TweetPost_ShouldCallAddTweet()
    {
        //arrange
        var mockService = new Mock<Core.Services.TweetService>();
        mockService.Setup (s => s.AddTweet(It.IsAny<PostTweetDTO>()))
            .Returns(new Tweet());
        var controller = new TweetServiceController(mockService.Object);
        
        //act
        controller.AddTweet(new PostTweetDTO());
        
        //assert
        mockService.Verify(s => s.AddTweet(It.IsAny<PostTweetDTO>()), Times.Once);
    }
}