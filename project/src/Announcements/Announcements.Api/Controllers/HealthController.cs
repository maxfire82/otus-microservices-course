using Microsoft.AspNetCore.Mvc;

namespace Announcements.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet(Name = "/")]
    public Object Get()
    {
        return new { Status = "OK" };
    }
}