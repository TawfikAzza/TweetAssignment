using Domain;
using Microsoft.EntityFrameworkCore;

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

    }
    
    public DbSet<Profile> ProfileTable { get; set; }
}