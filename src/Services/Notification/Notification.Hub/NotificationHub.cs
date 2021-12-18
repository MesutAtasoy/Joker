using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Notification.Hub;

[Authorize]
public class NotificationHub : Microsoft.AspNetCore.SignalR.Hub
{

    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, GetGroupName(Context));
        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupName(Context));
        await base.OnDisconnectedAsync(exception);
    }

    private string GetGroupName(HubCallerContext context)
    {
        var group = context?.User?.Claims.FirstOrDefault(c => c.Type == "organizationId")?.Value;
        return group;
    }
}