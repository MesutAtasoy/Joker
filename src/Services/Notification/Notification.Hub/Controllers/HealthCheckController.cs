﻿using Microsoft.AspNetCore.Mvc;

namespace Notification.Hub.Controllers;

[ApiVersion("1")]
[Route("api/HealthCheck")]
public class HealthCheckController :  ControllerBase
{

    [HttpGet("api-status")]
    [HttpHead("api-status")]
    public ActionResult ApiStatus()
    {
        return Ok("Notification Hub is awake!");
    }
}