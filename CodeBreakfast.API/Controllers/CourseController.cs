using System.Security.Claims;
using CodeBreakfast.Common.Models;
using CodeBreakfast.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreakfast.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]/[action]")]
public class CourseController(ICourseService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var rData = await service.GetAllAsync();
        return StatusCode((int)rData.StatusCode, new { rData.Success, rData.Data, rData.Message });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var rData = await service.GetByIdAsync(id);
        return StatusCode((int)rData.StatusCode, new { rData.Success, rData.Data, rData.Message });
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(AddCourseDto dto)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await service.AddAsync(dto, currentUserId);
        return StatusCode((int)rData.StatusCode, new { rData.Success, rData.Data, rData.Message });
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(UpdateCourseDto dto)
    {
        var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var rData = await service.UpdateAsync(dto);
        return StatusCode((int)rData.StatusCode, new { rData.Success, rData.Data, rData.Message });
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var rData = await service.DeleteAsync(id);
        return StatusCode((int)rData.StatusCode, new { rData.Success, rData.Data, rData.Message });
    }
}