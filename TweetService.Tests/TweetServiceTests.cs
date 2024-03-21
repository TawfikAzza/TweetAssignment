using Xunit;
using Moq;
using TweetService.Core.Services;
using TweetService.Core.Repositories;
using Domain;
using Domain.DTO;
using System;
using Domain.Helpers;
using EasyNetQ;

namespace TweetService.Tests
{
    public class TweetServiceTests
    {
        private Mock<ITweetRepository> _repositoryMock;
        private Mock<Publisher> _publisherMock;
        private Core.Services.TweetService _service;

        public TweetServiceTests()
        {
            _repositoryMock = new Mock<ITweetRepository>();
            _publisherMock = new Mock<Publisher>();
            _service = new Core.Services.TweetService(_repositoryMock.Object, _publisherMock.Object);
        }

        [Fact]
        public void AddTweet_ShouldReturnAddedTweet()
        {
            // Arrange
            var tweetDto = new PostTweetDTO
            {
                Text = "Test tweet", 
                CreatedAt = DateTime.Now, 
                UserId = 1
            };
            var expectedTweet = new Tweet
            {
                Text = tweetDto.Text, 
                CreatedAt = tweetDto.CreatedAt, 
                UserId = tweetDto.UserId
            };
            
            _repositoryMock.Setup(r => r.AddTweet(It.IsAny<Tweet>())).Returns(expectedTweet);
            _publisherMock.Setup(p => p.PublishMessageAsync(It.IsAny<TweetMessage>(), It.IsAny<string>()));

            // Act
            var result = _service.AddTweet(tweetDto);

            // Assert
            Assert.Equal(expectedTweet, result);
            _repositoryMock.Verify(r => r.AddTweet(It.IsAny<Tweet>()), Times.Once);
            _publisherMock.Verify(p => p.PublishMessageAsync(It.IsAny<TweetMessage>(), It.IsAny<string>()), Times.Once);
        }
    }
}