using Joker.EventBus;

namespace Notification.Application.UserNotifications.Events;

public class NewNotificationCreatedEvent : IntegrationEvent
{
    private NewNotificationCreatedEvent()
    {
        
    }
    public NewNotificationCreatedEvent(Guid ownerId, string title, string content)
    {
        OwnerId = ownerId;
        Title = title;
        Content = content;
    }
    
    public Guid OwnerId { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
}