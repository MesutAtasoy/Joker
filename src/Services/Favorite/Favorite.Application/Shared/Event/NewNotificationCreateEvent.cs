using System;
using Joker.EventBus;

namespace Favorite.Application.Shared.Event;

public class NewNotificationCreateEvent : IntegrationEvent
{
    private NewNotificationCreateEvent(){}
    
    public NewNotificationCreateEvent(Guid ownerId, 
        string title,
        string content)
    {
        OwnerId = ownerId;
        Title = title;
        Content = content;
    }
    
    public Guid OwnerId { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
}