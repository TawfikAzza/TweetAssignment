using Domain;
using Microsoft.EntityFrameworkCore;

namespace UserService.Core.Repositories;

public class UserServiceContext : DbContext
{
    public UserServiceContext(DbContextOptions<UserServiceContext> options) : base(options)
    {
        
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        
    }
    
    public DbSet<User> UserTable { get; set; }
}