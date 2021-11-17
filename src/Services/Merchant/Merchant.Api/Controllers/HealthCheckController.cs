using Microsoft.AspNetCore.Mvc;

namespace Merchant.Api.Controllers;

[ApiVersion("1")]
[Route("api/HealthCheck")]
public class HealthCheckController :  ControllerBase
{

    [HttpGet("api-status")]
    [HttpHead("api-status")]
    public ActionResult ApiStatus()
    {
        return Ok("Merchant Api is awake!");
    }
}