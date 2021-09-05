using Microsoft.AspNetCore.Mvc;

namespace Favorite.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/HealthCheck")]
    public class HealthCheckController :  ControllerBase
    {

        [HttpGet("api-status")]
        [HttpHead("api-status")]
        public ActionResult ApiStatus()
        {
            return Ok("Favorite Api is awake!");
        }
    }
}