using Microsoft.AspNetCore.SignalR;

namespace Joker.WebApp.Hubs;

public class CampaignCreatedNotificationHub : Hub
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }
    public override Task OnDisconnectedAsync(Exception exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}