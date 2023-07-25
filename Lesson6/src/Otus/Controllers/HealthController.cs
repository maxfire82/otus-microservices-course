using Microsoft.AspNetCore.Mvc;

namespace Otus.Controllers;

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