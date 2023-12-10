using Microsoft.AspNetCore.Mvc;

namespace Search.Api.Controllers;

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