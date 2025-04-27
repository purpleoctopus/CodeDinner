using CodeBreakfast.DataLayer.Enums;

namespace CodeBreakfast.Common.Models;

public class CourseAddDto
{
    public string Name { get; set; }
    
    public CourseLanguage Language { get; set; }
}

public class CourseUpdateDto : CourseAddDto
{
    public Guid Id { get; set; }
}

public class CourseDetailDto : CourseUpdateDto
{
    public List<string>? Modules { get; set; }
}