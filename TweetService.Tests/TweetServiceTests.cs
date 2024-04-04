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
            
            //TODO: Make the argument matcher more specific
            _repositoryMock.Setup(r => r.AddTweet(It.IsAny<Tweet>())).Returns(expectedTweet);
            _publisherMock.Setup(p => p.PublishMessageAsync(It.IsAny<TweetMessage>(), It.IsAny<string>()));

            // Act
            var result = _service.AddTweet(tweetDto);

            // Assert
            Assert.Equal(expectedTweet, result);
            _repositoryMock.Verify(r => r.AddTweet(It.IsAny<Tweet>()), Times.Once);
            _publisherMock.Verify(p => p.PublishMessageAsync(It.IsAny<TweetMessage>(), It.IsAny<string>()), Times.Once);
        }
        
        [Fact]
        public void DeleteTweet_ShouldDeleteTweet()
        {
            // Arrange
            var tweetId = 1;
            var tweet = new Tweet
            {
                Id = tweetId
            };
            _repositoryMock.Setup(r => r.GetTweet(tweetId)).Returns(tweet);
            _publisherMock.Setup(p => p.PublishMessageAsync(It.IsAny<TweetMessage>(), It.IsAny<string>()));

            // Act
            _service.DeleteTweet(tweetId);

            // Assert
            _repositoryMock.Verify(r => r.DeleteTweet(tweetId), Times.Once);
        }
        
        [Fact]
        public void DeleteTweet_ShouldThrowKeyNotFoundException_WhenTweetNotFound()
        {
            // Arrange
            var tweetId = 1;
    
            // Setup mock behavior to throw KeyNotFoundException
            _repositoryMock.Setup(r => r.GetTweet(tweetId)).Throws(new KeyNotFoundException());

            // Act & Assert
            // Verify that invoking DeleteTweet(tweetId) throws KeyNotFoundException
            Assert.Throws<KeyNotFoundException>(() => _service.DeleteTweet(tweetId));

            // Verify that GetTweet(tweetId) was called once
            _repositoryMock.Verify(repository => repository.GetTweet(tweetId), Times.Once);

            // Verify that no other methods were called on the repository or publisher mocks
            _repositoryMock.VerifyNoOtherCalls();
            _publisherMock.VerifyNoOtherCalls();
        }
        
        [Fact]
        public void GetTweets_ShouldReturnTweets()
        {
            // Arrange
            var userId = 1;
            var expectedTweets = new List<Tweet>
            {
                new Tweet { UserId = userId },
                new Tweet { UserId = userId }
            };
            _repositoryMock.Setup(r => r.GetTweets(userId)).Returns(expectedTweets);

            // Act
            var result = _service.GetTweets(userId);

            // Assert
            Assert.Equal(expectedTweets, result);
            _repositoryMock.Verify(r => r.GetTweets(userId), Times.Once);
        }

    }
}