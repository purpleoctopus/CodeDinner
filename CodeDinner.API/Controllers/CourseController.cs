using CodeDinner.API.Models.DTOs;
using CodeDinner.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeDinner.API.Controllers;

[ApiController]
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
        var rData = await service.AddAsync(dto);
        return StatusCode((int)rData.StatusCode, new { rData.Success, rData.Data, rData.Message });
    }
    
    [HttpPut]
    public async Task<IActionResult> Update(UpdateCourseDto dto)
    {
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