using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.UserNotifications.Queries.GetUserNotifications;

namespace Notification.Api.Controllers;

[ApiVersion("1")]
[Route("api/UserNotifications")]
public class UserNotificationController :  ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserNotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Returns user notifications by owner id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        return Ok(await _mediator.Send(new GetUserNotificationsQuery(id)));
    }
}