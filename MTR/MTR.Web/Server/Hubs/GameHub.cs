using Microsoft.AspNetCore.SignalR;

namespace MTR.Web.Server.Hubs;

public class GameHub : Hub
{
    public async Task RoundReady(Guid playerGuid, bool isReady)
    {
        await Clients.All.SendAsync("PlayerReadyChanged", playerGuid, isReady);
    }
}
