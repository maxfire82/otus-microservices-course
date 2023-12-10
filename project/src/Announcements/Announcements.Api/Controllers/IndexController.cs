using Microsoft.AspNetCore.Mvc;

namespace Announcements.Api.Controllers;

[ApiController]
[Route("")]
public class IndexController : ControllerBase
{
    [HttpGet]
    public Object Get()
    {
        return new { Status = "OK" };
    }
}