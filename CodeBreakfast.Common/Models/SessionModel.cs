using CodeBreakfast.Data;

namespace CodeBreakfast.Common.Models;

public class SessionModel
{
    public string Username { get; set; }
    public string AccessToken { get; set; }
    public List<AppRole> Roles { get; set; }
}