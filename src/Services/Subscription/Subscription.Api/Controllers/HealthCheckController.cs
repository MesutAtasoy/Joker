using Microsoft.AspNetCore.Mvc;

namespace Subscription.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/HealthCheck")]
    public class HealthCheckController :  ControllerBase
    {

        [HttpGet("api-status")]
        [HttpHead("api-status")]
        public ActionResult ApiStatus()
        {
            return Ok("Subscription Api is awake!");
        }
    }
}