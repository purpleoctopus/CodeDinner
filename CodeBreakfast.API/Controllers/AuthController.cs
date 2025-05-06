using CodeBreakfast.Common.Models;
using CodeBreakfast.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreakfast.API.Controllers;

[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var rData = await authService.Login(dto);
        return StatusCode((int)rData.StatusCode, rData);
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var rData = await authService.Register(dto);
        return StatusCode((int)rData.StatusCode, rData);
    }
}