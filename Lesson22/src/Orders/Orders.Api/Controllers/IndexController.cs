using Microsoft.AspNetCore.Mvc;

namespace Orders.Api.Controllers;

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