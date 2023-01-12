﻿using AutoMapper;

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
    public async Task AddGroupAsync(string gameGuid)
    {
        var player = await _mediator.Send(new GetPlayerQuery { GameGuid = new Guid(gameGuid), Username = Context.User.Identity.Name });

        if (player is null)
        {
            return;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, gameGuid);
        await Clients.Caller.SendAsync("JoinGroupCallback", player.Guid);
        await Clients.OthersInGroup(gameGuid).SendAsync("PlayerJoined", player);
    }

    [Authorize]
    [HubMethodName("LeaveGroup")]
    public async Task LeaveGroupAsync(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        //await Clients.Group(groupName).SendAsync("PlayerRemoved", playerGuid);
    }

    public override async Task OnConnectedAsync()
    {
        string name = Context.User.Identity.Name;

        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        string name = Context.User.Identity.Name;

        return base.OnDisconnectedAsync(exception);
    }
}
