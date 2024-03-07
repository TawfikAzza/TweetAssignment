using Domain;
using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers;

[ApiController]
[Route("[controller]")]
public class UserServiceController : ControllerBase
{
    private readonly Core.Services.UserService _service;
    public UserServiceController(Core.Services.UserService service)
    {
        _service = service;
    }
    [HttpGet("rebuildDB")]
    public void RebuildDB()
    {
        _service.RebuildDB();
    }

    [HttpPost("User")]
    public void AddUser(User user)
    {
        _service.AddUser(user);
    }
    
    [HttpDelete("User")]
    public void DeleteUser(int userId)
    {
        _service.DeleteUser(userId);
    }
    
    [HttpGet("User")]
    public User GetUser(int userId)
    {
        return _service.GetUser(userId);
    }
 
    
}