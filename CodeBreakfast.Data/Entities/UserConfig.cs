using System.ComponentModel.DataAnnotations.Schema;

namespace CodeBreakfast.Data.Entities;

public class UserConfig
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public UserConfigKey Key { get; set; }
    public string Value { get; set; }
}