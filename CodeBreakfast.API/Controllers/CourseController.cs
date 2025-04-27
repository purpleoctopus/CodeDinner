using System.Security.Claims;
using CodeBreakfast.Common.Models;
using CodeBreakfast.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreakfast.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CourseController(ICourseService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get_Courses_All()
    {
        var rData = await service.GetAllAsync();
        return StatusCode((int)rData.StatusCode, new { rData.Success, rData.Data, rData.Message });
    }

    [HttpGet]
    [Route("for-list-view")]
    public async Task<IActionResult> Get_Courses_ForListView()
    {
        var rData = await service.Get_ForListViewAsync();
        return StatusCode((int)rData.StatusCode, new { rData.Success, rData.Data, rData.Message });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get_Course_ById(Guid id)
    {
        var rData = await service.GetByIdAsync(id);
        return StatusCode((int)rData.StatusCode, new { rData.Success, rData.Data, rData.Message });
    }
    
    [HttpPost]
    public async Task<IActionResult> Add_Course(CourseAddDto dto)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await service.AddAsync(dto, currentUserId);
        return StatusCode((int)rData.StatusCode, new { rData.Success, rData.Data, rData.Message });
    }
    
    [HttpPut]
    public async Task<IActionResult> Update_Course(CourseUpdateDto dto)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await service.UpdateAsync(dto);
        return StatusCode((int)rData.StatusCode, new { rData.Success, rData.Data, rData.Message });
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete_Course(Guid id)
    {
        var rData = await service.DeleteAsync(id);
        return StatusCode((int)rData.StatusCode, new { rData.Success, rData.Data, rData.Message });
    }
}