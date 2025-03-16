using CodeDinner.API.Exceptions;
using CodeDinner.API.Models.DTO;
using CodeDinner.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeDinner.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CourseController(ICourseService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await service.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCourseDto dto)
    {
        try
        {
            await service.CreateAsync(dto);
            return Ok();
        }
        catch (StatusCodeException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            return Ok(await service.GetByIdAsync(id));
        }
        catch (StatusCodeException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
    }
}