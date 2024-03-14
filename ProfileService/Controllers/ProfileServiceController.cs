using Domain;
using Microsoft.AspNetCore.Mvc;

namespace ProfileService.Controllers;

[ApiController]
[Route("[controller]")]
public class ProfileServiceController : ControllerBase
{
    private readonly ProfileService.Core.Services.ProfileService _service;
    public ProfileServiceController(ProfileService.Core.Services.ProfileService service)
    {
        _service = service;
    }
    
    [HttpGet("Profile")]
    public Profile GetProfile(int userId)
    {
        return _service.GetProfile(userId);
    }
    
    [HttpGet("rebuildDB")]
    public void rebuildDB()
    {
        _service.rebuildDB();
    }
   
}