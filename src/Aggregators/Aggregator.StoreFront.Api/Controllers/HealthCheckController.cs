using Microsoft.AspNetCore.Mvc;

namespace Aggregator.StoreFront.Api.Controllers;

[ApiVersion("1")]
[Route("api/HealthCheck")]
public class HealthCheckController :  ControllerBase
{

    [HttpGet("api-status")]
    [HttpHead("api-status")]
    public ActionResult ApiStatus()
    {
        return Ok("Aggregator Api is awake!");
    }
}