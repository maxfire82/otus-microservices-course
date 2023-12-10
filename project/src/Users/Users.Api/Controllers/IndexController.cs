using Microsoft.AspNetCore.Mvc;

namespace Users.Api.Controllers;

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