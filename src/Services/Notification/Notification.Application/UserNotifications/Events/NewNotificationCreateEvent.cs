using Joker.EventBus;

namespace Notification.Application.UserNotifications.Events;

public class NewNotificationCreateEvent : IntegrationEvent
{
    private NewNotificationCreateEvent()
    {
    }
    
    public Guid OwnerId { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
}