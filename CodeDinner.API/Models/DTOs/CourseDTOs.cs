using CodeDinner.API.Entities;
using CodeDinner.API.Enums;

namespace CodeDinner.API.Models.DTOs;

public class AddCourseDto
{
    public string Name { get; set; }
    
    public CourseLanguage Language { get; set; }
}

public class UpdateCourseDto : AddCourseDto
{
    public Guid Id { get; set; }
    public List<Module>? Modules { get; set; }
}