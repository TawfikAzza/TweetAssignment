using Domain;
using ProfileService.Core.Repositories;

namespace ProfileService.Core.Services;

public class ProfileService
{
    private readonly ProfileServiceRepository _repository;
    
    public ProfileService(ProfileServiceRepository repository)
    {
        _repository = repository;
    }
    
    public Profile GetProfile(int userId)
    {
        return _repository.GetProfile(userId);
    }
    public Profile UpdateProfile(Profile profile)
    {
        return _repository.UpdateProfile(profile);
    }
    public void rebuildDB()
    {
        _repository.rebuildDB();
    }

    public void AddOrUpdateTweetToProfile(Profile profile, Tweet tweet)
    {
        _repository.AddOrUpdateTweetToProfile(profile, tweet);
    }
    public void AddProfile(Profile profile)
    {
        _repository.AddProfile(profile);
    }

    public void DeleteTweetFromProfile(Profile profile, Tweet tweetToDelete)
    {
        _repository.DeleteTweetFromProfile(profile, tweetToDelete);
    }
}