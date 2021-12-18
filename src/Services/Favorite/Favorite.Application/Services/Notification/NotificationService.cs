using System;
using System.Threading.Tasks;
using Favorite.Application.Shared.Event;
using Joker.EventBus;

namespace Favorite.Application.Services.Notification;

public class NotificationService : INotificationService
{
    private readonly IEventDispatcher _eventDispatcher;

    public NotificationService(IEventDispatcher eventDispatcher)
    {
        _eventDispatcher = eventDispatcher;
    }

    public async Task SendAsync(Guid ownerId, string title, string content)
    {
        await _eventDispatcher.Dispatch(new NewNotificationCreateEvent(ownerId, title, content));
    }
}