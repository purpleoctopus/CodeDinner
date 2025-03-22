using Microsoft.AspNetCore.Mvc;

namespace CodeDinner.API.Controllers;

public class UserController : ControllerBase
{
    public UserController()
    {
        
    }
    
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Register()
    {
        return Ok();
    }
}