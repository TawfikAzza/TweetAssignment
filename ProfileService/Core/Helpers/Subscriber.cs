using Domain;
using Domain.Helpers;
using EasyNetQ;

namespace ProfileService.Core.Helpers;

public class Subscriber
{
    private readonly IBus bus;
    private readonly Services.ProfileService _service;
    private IBus _bus;
    public Subscriber(IBus bus, Services.ProfileService service)
    {
        _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    public Subscriber(Services.ProfileService service)
    {
        _service = service;
    }

    public async Task Subscribe(string topic)
    {
        await _bus.PubSub.SubscribeAsync<ITMessage>(topic, HandleMessage, x => x.WithTopic(topic));
        Console.WriteLine($"Subscribed to {topic}");
    }
    public async Task SubscribeToAddTweet()
    {
        await _bus.PubSub.SubscribeAsync<ITMessage>("tweet.add", HandleMessageToAddTweet, x => x.WithTopic("tweet.add"));
        Console.WriteLine($"Subscribed to tweet.add");
    }
    public async Task SubscribeToDeleteTweet()
    {
        await _bus.PubSub.SubscribeAsync<ITMessage>("tweet.delete", HandleMessageToDeleteTweet, x => x.WithTopic("tweet.delete"));
        Console.WriteLine($"Subscribed to tweet.delete");
    }
    public async Task SubscribeToEditUser()
    {
        await _bus.PubSub.SubscribeAsync<ITMessage>("user.edit", HandleMessageToEditUser, x => x.WithTopic("user.edit"));
        Console.WriteLine($"Subscribed to user.edit");
    }
    public async Task SubscribeToAddUser()
    {
        
        //bus.PubSub.Subscribe<TMessage>("1", HandleMessage, x => x.WithTopic(topic));
        await _bus.PubSub.SubscribeAsync<ITMessage>("user.add", HandleMessageToAddUser, x => x.WithTopic("user.add"));
        Console.WriteLine($"Subscribed to user.add");
    }
    private void HandleMessageToEditUser(ITMessage message)
    {
        var userMessage = message as UserMessage;
        Console.WriteLine("Received In EditUser:  {message.MessageType}");
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
        Console.WriteLine($"Received In AddToUser:  {message.MessageType}");
        
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
        Console.WriteLine("Received In Add Tweet:  {message.MessageType}");
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
        Console.WriteLine("Received In Delete Tweet:  {message.MessageType}");
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
    private void HandleMessage(ITMessage message)
    {
        Console.WriteLine($"Received In Profile:  {message.MessageType}");
    }
}