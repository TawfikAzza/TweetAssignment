using Domain;

namespace ProfileService.Core.Repositories;

public class ProfileServiceRepository
{
    private readonly ProfileServiceContext _context;
    public ProfileServiceRepository(ProfileServiceContext context)
    {
        _context = context;
    }

    public Profile GetProfile(int userId)
    {
        return _context.ProfileTable.Find(userId);
    }
}