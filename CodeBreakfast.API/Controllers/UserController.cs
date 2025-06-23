using System.Security.Claims;
using CodeBreakfast.Common.Models;
using CodeBreakfast.Data;
using CodeBreakfast.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreakfast.API.Controllers;

[ApiController]
[Route("user")]
[Authorize]
public class UserController(IUserService userService, IUserActivityService userActivityService) : ControllerBase
{
    [HttpGet]
    [Route("all")]
    [Authorize(Roles = "Moderator,Admin")]
    public async Task<IActionResult> GetUsersList()
    {
        var rData = await userService.GetUsersList();
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserProfileForView(Guid id)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await userService.GetUserProfileForView(requestingUserId, id);
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpGet]
    [Route("me")]
    public async Task<IActionResult> GetMyProfileForView()
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await userService.GetUserProfileForView(requestingUserId, requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpGet]
    [Route("count-statistics")]
    public async Task<IActionResult> GetUserCountStatistics()
    {
        var rData = await userService.GetUserCountStatistics();
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpPut]
    [Route("me")]
    public async Task<IActionResult> UpdateMyUser([FromBody] UserUpdateDto dto)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await userService.UpdateUser(requestingUserId, dto);
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpPut]
    [Route("{id:guid}/role")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUserRole(Guid id, [FromBody] AppRole role)
    {
        var rData = await userService.UpdateUserRole(id,role);
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpGet]
    [Route("me/picture")]
    public async Task<IActionResult> GetMyProfilePicture()
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await userService.GetUserProfilePicture(requestingUserId, requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpPost]
    [Route("me/picture")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadMyProfilePicture([FromForm] UploadFileRequest file)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await userService.UploadUserProfilePicture(requestingUserId, file.File);
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpGet]
    [Route("{id:guid}/picture")]
    public async Task<IActionResult> GetUserProfilePicture(Guid id)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await userService.GetUserProfilePicture(requestingUserId, id);
        return StatusCode((int)rData.StatusCode, rData);
    }

[HttpGet]
    [Route("{id:guid}/activities")]
    public async Task<IActionResult> GetUserActivities(Guid id)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await userActivityService.GetUserActivityListAsync(id, requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }
    
    [HttpGet]
    [Route("me/activities")]
    public async Task<IActionResult> GetMyUserActivities()
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await userActivityService.GetUserActivityListAsync(requestingUserId, requestingUserId);
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
    
    // User Configuration
    [HttpGet]
    [Route("me/settings")]
    public async Task<IActionResult> GetMyUserConfiguration()
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await userService.GetUserConfiguration(requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpPost]
    [Route("me/settings")]
    public async Task<IActionResult> UpdateMyUserConfiguration([FromBody] List<UserConfigUpdateDto> userConfigs)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await userService.UpdateUserConfiguration(requestingUserId, userConfigs);
        return StatusCode((int)rData.StatusCode, rData);
    }
}