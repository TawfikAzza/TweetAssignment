using Domain;
using UserService.Core.Repositories;

namespace UserService.Core.Services;

public class UserService
{
    private readonly UserServiceRepository _repository;
    public UserService(UserServiceRepository repository)
    {
        _repository = repository;
    }
 

    public void RebuildDB()
    {
        _repository.RebuildDB();
    }

    public User AddUser(User user)
    {
        User addedUser = _repository.AddUser(user);
        //TODO: Send message to ProfileService to add the user to the cache
        return addedUser;
    }
    
    public User EditUser(User user)
    {
        User editedUser = _repository.EditUser(user);
        //TODO: Send message to ProfileService to update the user in the cache
        return editedUser;
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