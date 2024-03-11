using Domain;
using ProfileService.Core.Repositories;

namespace ProfileService.Core.Services;

public class ProfilService
{
    private readonly ProfileServiceRepository _repository;
    
    public ProfilService(ProfileServiceRepository repository)
    {
        _repository = repository;
    }

    public Profile GetProfile(int userId)
    {
        return _repository.GetProfile(userId);
    }

    public void rebuildDB()
    {
        _repository.rebuildDB();
    }
}