using Domain;
using Domain.Helpers;
using EasyNetQ;

namespace ProfileService.Core.Helpers;

public class MessageHandler : BackgroundService
{
    private readonly Services.ProfileService _service;
    private readonly IBus _bus;
    public MessageHandler(IBus bus, Services.ProfileService service)
    {
        _service = service;
        _bus = bus;
    }
 
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("MessageHandler is running.");
        var messageClient = new MessageClient(_bus);
        messageClient.Listen<ITMessage>(HandleMessageToAddUser, "user.add");
        messageClient.Listen<ITMessage>(HandleMessageToEditUser, "user.edit");
        messageClient.Listen<ITMessage>(HandleMessageToAddTweet, "tweet.add");
        messageClient.Listen<ITMessage>(HandleMessageToDeleteTweet, "tweet.delete");
        
        while(!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(1000, stoppingToken);
        }
        Console.WriteLine("MessageHandler is running.");
    }
    private void HandleMessageToEditUser(ITMessage message)
    {
        var userMessage = message as UserMessage;
        Console.WriteLine("Received In EditUser Message Handler:  {message.MessageType}");
        Profile profile = _service.GetProfile(userMessage.Id);
        if (profile != null)
        {
            profile.Username = userMessage.Username;
            profile.UserId = userMessage.Id;
            profile.Bio = userMessage.Bio;
        
            _service.UpdateProfile(profile);
        }

        
    }
    private void HandleMessageToAddUser(ITMessage message)
    {
        Console.WriteLine($"Received In AddToUser Message Handler:  {message.MessageType}");
        
        var userMessage = message as UserMessage;
        Console.WriteLine($"User Id: {userMessage.Id} Username: {userMessage.Username} Bio: {userMessage.Bio}");
        Profile profile = new Profile();
        profile.Username = userMessage.Username;
        profile.UserId = userMessage.Id;
        profile.Bio = userMessage.Bio;
        _service.AddProfile(profile);
    }
    private void HandleMessageToAddTweet(ITMessage message)
    {
        var tweetMessage = message as TweetMessage;
        Console.WriteLine("Received In Add Tweet Message Handler:  {message.MessageType}");
        Console.WriteLine($"Tweet Id: {tweetMessage.Id} UserId: {tweetMessage.UserId} Text: {tweetMessage.Text} CreatedAt: {tweetMessage.CreatedAt}");
        Profile profile = _service.GetProfile(tweetMessage.UserId);
        if (profile != null)
        {
            Console.WriteLine("Profile Found");
            var newTweet = new Tweet
            {
                Text = tweetMessage.Text,
                CreatedAt = tweetMessage.CreatedAt,
                UserId = tweetMessage.UserId,
                Id = tweetMessage.Id
            };
            
            _service.AddOrUpdateTweetToProfile(profile,newTweet);
        }
    }
    private void HandleMessageToDeleteTweet(ITMessage message)
    {
        var tweetMessage = message as TweetMessage;
        Console.WriteLine("Received In Delete Tweet Message Handler:  {message.MessageType}");
        Profile profile = _service.GetProfile(tweetMessage.UserId);
        if (profile != null)
        {
            var tweetToDelete = new Tweet
            {
                Text = tweetMessage.Text,
                CreatedAt = tweetMessage.CreatedAt,
                UserId = tweetMessage.UserId
            };
            
            _service.DeleteTweetFromProfile(profile,tweetToDelete);
        }
    }
    
}