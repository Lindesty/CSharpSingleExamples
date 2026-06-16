using System.Runtime.InteropServices;
using _5_WpfWithAspNetCore.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace _5_WpfWithAspNetCore.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class GreetingController(GreetingService greetingService) : ControllerBase
{
    [HttpGet("hello")]
    public ActionResult<Share.GreetingMessage> Hello([FromQuery] string? name)
    {
        return Ok(greetingService.CreateGreeting(name));
    }
}