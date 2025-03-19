using CodeDinner.API.Enums;

namespace CodeDinner.API.DTOs;

public class AddCourseDto
{
    public string Name { get; set; }
    
    public CourseLanguage Language { get; set; }
}

public class UpdateCourseDto : AddCourseDto
{
    public Guid Id { get; set; }
}