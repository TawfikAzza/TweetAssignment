using Domain;
using Microsoft.EntityFrameworkCore;

namespace TweetService.Core.Repositories;

public class TweeServiceContext : DbContext
{
    public TweeServiceContext(DbContextOptions<TweeServiceContext> options) : base(options)
    {
        
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tweet>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
    }
    
    public DbSet<Tweet> TweetTable { get; set; }
}