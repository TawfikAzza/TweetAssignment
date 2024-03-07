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

    public void AddUser(User user)
    {
        _repository.AddUser(user);
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