using Microsoft.AspNetCore.Mvc;

namespace Villsource.Server.Controllers;

[Route("api")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet("name")]
    public IActionResult GetName()
    {
        return Ok(new
        {
            value = "Villsource"
        });
    }

    [HttpGet("version")]
    public IActionResult GetVersion()
    {
        return Ok(new
        {
            value = "3.0.0"
        });
    }
}
