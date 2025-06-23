using System.Security.Claims;
using CodeBreakfast.Common.Models;
using CodeBreakfast.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreakfast.API.Controllers;

[ApiController]
[Authorize]
[Route("course/lesson")]
public class LessonController(ILessonService lessonService) : ControllerBase
{
    [HttpGet]
    [Route("get-all/{courseId:guid}")]
    [Authorize(Roles = "Moderator,Admin")]
    public async Task<IActionResult> Get_Lessons_All(Guid courseId)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await lessonService.GetAllForCourseAsync(courseId, requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpGet]
    [Route("for-list-view/{courseId:guid}")]
    public async Task<IActionResult> Get_Lessons_ForListView(Guid courseId)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await lessonService.GetForListViewAsync(courseId, requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get_Lesson_ById(Guid id)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await lessonService.GetByIdAsync(id, requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpPost]
    [Authorize(Roles = "Creator")]
    public async Task<IActionResult> Add_Lesson([FromBody] LessonAddDto dto)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await lessonService.AddAsync(dto, requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Creator")]
    public async Task<IActionResult> Update_Lesson(Guid id, [FromBody] LessonUpdateDto dto)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        if (id != dto.Id)
        {
            return BadRequest(new { Success = false, Message = "ID mismatch" });
        }
        
        var rData = await lessonService.UpdateAsync(dto, requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Creator")]
    public async Task<IActionResult> Delete_Lesson(Guid id)
    {
        var requestingUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await lessonService.DeleteAsync(id, requestingUserId);
        return StatusCode((int)rData.StatusCode, rData);
    }
}