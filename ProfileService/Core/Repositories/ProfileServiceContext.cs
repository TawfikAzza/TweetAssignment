using Domain;
using Microsoft.EntityFrameworkCore;
using ProfileService.Core.Helpers;

namespace ProfileService.Core.Repositories;

public class ProfileServiceContext : DbContext
{
    public ProfileServiceContext(DbContextOptions<ProfileServiceContext> options) : base(options)
    {
        
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Profile>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<ProfileTweet>()
            .Ignore(p => p.Profile)
            .Property(p=> p.Id)
            .ValueGeneratedOnAdd()
            ;
    }
    
    public DbSet<Profile> ProfileTable { get; set; }
    public DbSet<ProfileTweet> ProfileTweetTable { get; set; }
}