using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

using MTR.Domain;
using MTR.Web.Shared.Queries;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MTR.Web.Server.Hubs;

public class GameHub : Hub
{
    private static Dictionary<string, string> _connections = new();
    private readonly IMediator _mediator;

    public GameHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HubMethodName("RoundReady")]
    public async Task RoundReadyAsync(string gameGuid, string playerGuid, bool isReady)
    {
        await Clients.Groups(gameGuid).SendAsync("PlayerReadyChanged", playerGuid, isReady);
    }

    [Authorize]
    [HubMethodName("JoinGroup")]
    public async Task AddToGroupAsync(string gameGuid)
    {
        var player = await _mediator.Send(new GetPlayerQuery { GameGuid = new Guid(gameGuid), Username = Context.User.Identity.Name });

        if (player is not null)
        {
            await Clients.Caller.SendAsync("JoinGameCallback", player.Guid);
        }

        _connections[Context.ConnectionId] = gameGuid;
        await Groups.AddToGroupAsync(Context.ConnectionId, gameGuid);
    }

    [Authorize]
    [HubMethodName("JoinGame")]
    public async Task AddToGameAsync(string gameGuid)
    {
        var query = new GetPlayerQuery { GameGuid = new Guid(gameGuid), Username = Context.User.Identity.Name };
        var player = await _mediator.Send(query);

        if (player is not null)
        {
            //await Clients.Caller.SendAsync("JoinGameCallback", player.Guid);
            await Clients.Group(gameGuid).SendAsync("PlayerJoined", player);
        }
    }

    [Authorize]
    [HubMethodName("LeaveGame")]
    public async Task LeaveGameAsync(string gameGuid, string playerGuid)
    {
        await Clients.Group(gameGuid).SendAsync("PlayerLeft", playerGuid);
    }

    [Authorize]
    [HubMethodName("LeaveGroup")]
    public async Task LeaveGroupAsync(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

        _connections.Remove(Context.ConnectionId);
        //await Clients.Group(groupName).SendAsync("PlayerRemoved", playerGuid);
    }

    public override async Task OnConnectedAsync()
    {
        _connections.Add(Context.ConnectionId, string.Empty);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await LeaveGroupAsync(_connections[Context.ConnectionId]);
        await base.OnDisconnectedAsync(exception);
    }
}
