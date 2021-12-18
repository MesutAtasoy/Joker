using Joker.BackOffice.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Joker.BackOffice.ViewComponents;

public class NotificationComponent :  ViewComponent
{
    private readonly INotificationService _notificationService;
    private readonly IUserService _userService;

    public NotificationComponent(INotificationService notificationService,
        IUserService userService)
    {
        _notificationService = notificationService;
        _userService = userService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var ownerId = _userService.GetOrganizationId();

        var unReadNotifications = await _notificationService.GetUserNotificationsAsync(ownerId, false);
        
        return View("Default", unReadNotifications);
    }
}