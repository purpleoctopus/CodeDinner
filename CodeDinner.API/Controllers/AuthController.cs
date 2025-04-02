using Microsoft.AspNetCore.Mvc;

namespace CodeDinner.API.Controllers;

[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    public AuthController()
    {
        
    }
    
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }
    
    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> Register()
    {
        return Ok();
    }
}