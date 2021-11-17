using DotNetCore.CAP;
using Joker.CAP.IntegrationEvent;
using Joker.WebApp.Events;
using Joker.WebApp.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Joker.WebApp.EventHandlers;

public class CampaignCreatedNotificationEventHandler : CAPIntegrationEventHandler<CampaignCreatedNotificationEvent>
{
    private readonly IHubContext<CampaignCreatedNotificationHub> _hubContext;

    public CampaignCreatedNotificationEventHandler(IHubContext<CampaignCreatedNotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [CapSubscribe(nameof(CampaignCreatedNotificationEvent))]
    public override async Task Handle(CampaignCreatedNotificationEvent @event)
    {
        await _hubContext.Clients.Users(@event.UserId).SendAsync("lastCampaignCreated", @event);
    }
}