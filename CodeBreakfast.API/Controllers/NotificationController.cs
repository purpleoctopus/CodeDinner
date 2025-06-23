using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreakfast.API.Controllers;

[ApiController]
[Authorize]
[Route("notification")]
public class NotificationController : ControllerBase
{
    
}