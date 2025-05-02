using CodeBreakfast.Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CodeBreakfast.Logic.Hubs;

[Authorize]
public class NotificationHub(ConnectionManager connectionManager) : Hub
{
    public override Task OnConnectedAsync()
    {
        connectionManager.AddConnection(Context.ConnectionId, Guid.Parse(Context.UserIdentifier));
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        connectionManager.RemoveConnection(Guid.Parse(Context.UserIdentifier));
        return base.OnDisconnectedAsync(exception);
    }
}