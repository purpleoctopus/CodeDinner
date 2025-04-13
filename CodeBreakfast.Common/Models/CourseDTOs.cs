using CodeBreakfast.DataLayer.Enums;

namespace CodeBreakfast.Common.Models;

public class AddCourseDto
{
    public string Name { get; set; }
    
    public CourseLanguage Language { get; set; }
}

public class UpdateCourseDto : AddCourseDto
{
    public Guid Id { get; set; }
    public List<string>? Modules { get; set; }
}