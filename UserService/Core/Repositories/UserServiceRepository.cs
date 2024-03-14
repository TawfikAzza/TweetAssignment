﻿using Domain;

namespace UserService.Core.Repositories;

public class UserServiceRepository
{
    private readonly UserServiceContext _context;
    public UserServiceRepository(UserServiceContext context)
    {
        _context = context;
        RebuildDB();
    }

    public void RebuildDB()
    {
        _context.Database.EnsureCreated();
    }

    public User AddUser(User user)
    {
        _context.UserTable.Add(user);
        _context.SaveChanges();
        return user;
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

    public User UpdateUser(User user)
    {
        _context.UserTable.Update(user);
        _context.SaveChanges();
        return user;
    }
}