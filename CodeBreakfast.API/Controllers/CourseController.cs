using System.Security.Claims;
using CodeBreakfast.Common.Models;
using CodeBreakfast.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreakfast.API.Controllers;

[ApiController]
[Authorize]
[Route("api/course")]
public class CourseController(ICourseService service) : ControllerBase
{
    [HttpGet]
    [Route("get-all")]
    [Authorize(Roles = "Moderator,Admin")]
    public async Task<IActionResult> Get_Courses_All()
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await service.GetAllForUserAsync(requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpGet]
    [Route("for-list-view")]
    public async Task<IActionResult> Get_Courses_ForListView()
    {
        var rData = await service.GetForListViewAsync();
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get_Course_ById(Guid id)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await service.GetByIdAsync(id, requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }
    
    [HttpPost]
    [Authorize(Roles = "Creator")]
    public async Task<IActionResult> Add_Course(CourseAddDto dto)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await service.AddAsync(dto, requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpPost]
    [Route("get-access/{id:guid}")]
    public async Task<IActionResult> Get_Course_Access(Guid id)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await service.AccessCourse(id, requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Creator")]
    public async Task<IActionResult> Update_Course(Guid id, CourseUpdateDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(new { Success = false, Message = "ID mismatch" });
        }
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await service.UpdateAsync(dto,requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Creator")]
    public async Task<IActionResult> Delete_Course(Guid id)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await service.DeleteAsync(id,requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }
}