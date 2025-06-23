using System.ComponentModel.DataAnnotations;
using CodeBreakfast.Data;

namespace CodeBreakfast.Common.Models;

public class UserUpdateDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    [Length(1,10)]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

public class UserDetailDto : UserUpdateDto
{
    public AppRole Role { get; set; }
}

public class UserProfileDto
{
    public Guid Id { get; set; }
    public AppRole Role { get; set; }
    public string Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? CoursesCount { get; set; }
    public int? CompletedCoursesCount { get; set; }
    public int? CreatedCoursesCount { get; set; }
    public DateTime RegisteredOn { get; set; }
    public bool IsPrivate { get; set; }
    public List<UserProfileSection> SectionsToView { get; set; } = [];
}

public class UserCountStatisticsDto
{
    public int TotalCount { get; set; }
    public int AdminCount { get; set; }
    public int ModeratorCount { get; set; }
    public int CreatorCount { get; set; }
}