using CodeBreakfast.Common.Models;
using CodeBreakfast.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreakfast.API.Controllers;

[Route("api/[controller]/[action]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var rData = await authService.Login(dto);
        return StatusCode((int)rData.StatusCode, new { rData.Success, rData.Data, rData.Message });
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var rData = await authService.Register(dto);
        return StatusCode((int)rData.StatusCode, new { rData.Success, rData.Data, rData.Message });
    }
}