using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.UserNotifications.Queries.GetUserNotifications;

namespace Notification.Api.Controllers;

[ApiVersion("1")]
[Route("api/UserNotifications")]
[Authorize]
public class UserNotificationController : ControllerBase
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
    /// <param name="type"></param>
    /// <returns></returns>
    [HttpGet("{id}/{type}")]
    public async Task<IActionResult> GetAsync(Guid id, string type)
    {
        bool? isRead = type.ToLowerInvariant() switch
        {
            "read" => true,
            "unread" => false,
            "all" => null,
            _ => null,
        };

        return Ok(await _mediator.Send(new GetUserNotificationsQuery(id, isRead)));
    }
}