using Microsoft.AspNetCore.Identity;

namespace CodeBreakfast.DataLayer.Entities;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<UserLesson> UserLessons { get; set; }
}