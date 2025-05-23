﻿using CodeBreakfast.Data;
using CodeBreakfast.Data.Entities;

namespace CodeBreakfast.Common.Models;

public class CourseAddDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public CourseLanguage Language { get; set; }
    public string? PrimarySpecialization { get; set; }
    public List<string> Tags { get; set; } = [];
}

public class CourseUpdateDto : CourseAddDto
{
    public Guid Id { get; set; }
    public bool IsVisible { get; set; }
    public List<Module>? Modules { get; set; }
}

public class CourseDetailDto : CourseUpdateDto
{
    public UserDetailDto Author { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}

public class CourseForListDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public CourseLanguage Language { get; set; }
    public UserDetailDto Author { get; set; }
    public int ModulesCount { get; set; }
    public int LessonsCount { get; set; }
    public int StudentsCount { get; set; }
    public TimeSpan TotalTime { get; set; }
    
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
}