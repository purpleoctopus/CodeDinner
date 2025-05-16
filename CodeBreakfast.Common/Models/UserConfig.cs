using CodeBreakfast.Data;

namespace CodeBreakfast.Common.Models;

public class UserConfigUpdateDto
{
    public UserConfigKey Key { get; set; }
    public string Value { get; set; }
}

public class UserConfigDetailDto : UserConfigUpdateDto
{
    public Guid UserId { get; set; }
}