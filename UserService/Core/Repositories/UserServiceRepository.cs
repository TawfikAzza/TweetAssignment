using Domain;

namespace UserService.Core.Repositories;

public class UserServiceRepository
{
    private readonly UserServiceContext _context;
    public UserServiceRepository(UserServiceContext context)
    {
        _context = context;
    }

    public void RebuildDB()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    public void AddUser(User user)
    {
        _context.UserTable.Add(user);
        _context.SaveChanges();
    }

    public void DeleteUser(int userId)
    {
        _context.UserTable.Remove(_context.UserTable.Find(userId));
        _context.SaveChanges();
    }

    public User GetUser(int userId)
    {
        return _context.UserTable.Find(userId);
    }
}