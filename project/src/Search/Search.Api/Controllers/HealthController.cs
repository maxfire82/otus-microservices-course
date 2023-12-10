using Microsoft.AspNetCore.Mvc;

namespace Search.Api.Controllers;

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