using Domain;
using Domain.Helpers;
using UserService.Core.Repositories;

namespace UserService.Core.Services;

public class UserService
{
    private readonly UserServiceRepository _repository;
    private readonly Subscriber _subscriber;
    private readonly Publisher _publisher;
    public UserService(UserServiceRepository repository)
    {
        _repository = repository;
        _subscriber = new Subscriber();
        _publisher = new Publisher();
    }
 

    public void RebuildDB()
    {
        _repository.RebuildDB();
    }

    public void AddUser(User user)
    {
        
        User userCreated = _repository.AddUser(user);
        Console.WriteLine($"User created {userCreated.Id} {userCreated.Username} {userCreated.Bio}");
        UserMessage userMessage = new UserMessage
        {
            MessageType = "User",
            Id = userCreated.Id,
            Username = userCreated.Username,
            Bio = userCreated.Bio
        };
        _publisher.PublishMessageAsync(userMessage, "user.add");
    }
    public User UpdateUser(User user)
    {
        UserMessage userMessage = new UserMessage
        {
            MessageType = "User",
            Id = user.Id,
            Username = user.Username,
            Bio = user.Bio
        };
        User userUpdated = _repository.UpdateUser(user);
        _publisher.PublishMessageAsync(userMessage, "user.edit");
        return userUpdated;


    }
    public void DeleteUser(int userId)
    {
        _repository.DeleteUser(userId);
    }

    public User GetUser(int userId)
    {
        return _repository.GetUser(userId);
    }
}