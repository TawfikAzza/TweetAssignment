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
    public ActionResult<User> AddUser(User user)
    {
        return _service.AddUser(user);
    }
    
    [HttpPut("User")]
    public ActionResult<User> EditUser(User user)
    {
        return _service.EditUser(user);
    }
    
    [HttpDelete("User")]
    public void DeleteUser(int userId)
    {
        _service.DeleteUser(userId);
    }
    
    [HttpGet("User")]
    public ActionResult<User> GetUser(int userId)
    {
        try {
            return _service.GetUser(userId);
        } catch (KeyNotFoundException e) {
            return NotFound(e.Message);
        }
    }
    
}