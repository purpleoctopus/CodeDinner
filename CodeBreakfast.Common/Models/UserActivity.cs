using CodeBreakfast.Data;

namespace CodeBreakfast.Common.Models;

public class UserActivityDetailDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public ActivityType ActivityType { get; set; }
    public string Title { get; set; }
    public string AdditionalJson { get; set; }
}