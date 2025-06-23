using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeBreakfast.API.Controllers;

[ApiController]
[Authorize]
[Route("message")]
public class MessageController : ControllerBase
{
    
}