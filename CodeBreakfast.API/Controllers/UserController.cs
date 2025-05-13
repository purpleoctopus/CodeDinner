using System.Security.Claims;
using CodeBreakfast.Common.Models;
using CodeBreakfast.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreakfast.API.Controllers;

[ApiController]
[Route("api/user")]
[Authorize]
public class UserController(IUserService userService, IUserActivityService userActivityService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserProfileForView(Guid id)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await userService.GetUserProfileForView(requestingUserId, id);
        return StatusCode((int)rData.StatusCode, rData);
    }
    
    [HttpGet]
    [Route("me")]
    public async Task<IActionResult> GetMyProfile()
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await userService.GetUserProfileForView(requestingUserId, requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }
    
    [HttpGet]
    [Route("{id:guid}/activities")]
    public async Task<IActionResult> GetUserActivities(Guid id)
    {
        var rData = await userActivityService.GetUserActivityListAsync(id);
        return StatusCode((int)rData.StatusCode, rData);
    }
    
    [HttpGet]
    [Route("me/activities")]
    public async Task<IActionResult> GetMyUserActivities()
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await userActivityService.GetUserActivityListAsync(requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }
    
    [HttpPost]
    [Route("me/activities")]
    public async Task<IActionResult> CreateUserActivityAsync(UserActivityDetailDto userActivity)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await userActivityService.CreateUserActivityAsync(userActivity, requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpDelete]
    [Route("me/activities/{id:guid}")]
    public async Task<IActionResult> DeleteUserActivityAsync(Guid id)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await userActivityService.DeleteUserActivityAsync(id, requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpDelete]
    [Route("me/activities/all")]
    public async Task<IActionResult> DeleteAllUserActivities()
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await userActivityService.DeleteAllUserActivitiesForUserAsync(requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }
}