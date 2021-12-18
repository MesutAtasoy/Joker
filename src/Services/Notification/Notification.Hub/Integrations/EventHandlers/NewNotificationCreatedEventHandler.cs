using DotNetCore.CAP;
using Joker.CAP.IntegrationEvent;
using Microsoft.AspNetCore.SignalR;
using Notification.Hub.Integrations.Events;

namespace Notification.Hub.Integrations.EventHandlers;

public class NewNotificationCreatedEventHandler :  CAPIntegrationEventHandler<NewNotificationCreatedEvent>
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NewNotificationCreatedEventHandler(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [CapSubscribe(nameof(NewNotificationCreatedEvent))]
    public override async Task Handle(NewNotificationCreatedEvent @event)
    {
        await _hubContext.Clients.Group(@event.OwnerId.ToString()).SendAsync("new", @event);
    }
}