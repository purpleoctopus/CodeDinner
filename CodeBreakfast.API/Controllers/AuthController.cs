using CodeBreakfast.Common.Models;
using CodeBreakfast.Logic.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreakfast.API.Controllers;

[Route("api/[controller]/[action]")]
public class AuthController(IUserService userService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var rData = await userService.Login(dto);
        return StatusCode((int)rData.StatusCode, new { rData.Success, rData.Data, rData.Message });
    }
    
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var rData = await userService.Register(dto);
        return StatusCode((int)rData.StatusCode, new { rData.Success, rData.Data, rData.Message });
    }
}