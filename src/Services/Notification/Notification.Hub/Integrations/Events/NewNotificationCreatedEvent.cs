using Joker.EventBus;

namespace Notification.Hub.Integrations.Events;

public class NewNotificationCreatedEvent : IntegrationEvent
{
    private NewNotificationCreatedEvent()
    {
        
    }
    
    public Guid OwnerId { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
}