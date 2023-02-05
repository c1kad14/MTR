using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

using MTR.Domain;
using MTR.DTO;
using MTR.Web.Server.Hubs;
using MTR.Web.Shared.Commands;
using MTR.Web.Shared.Models;
using MTR.Web.Shared.Queries;

namespace MTR.Web.Server.Controllers;

[Route("[controller]")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<MTRUser> _userManager;

    public GameController(IMediator mediator, UserManager<MTRUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    [HttpPost]
    [Route("GetGames")]
    public async Task<ResponseMultiple<GameListItemDto>> GetGames([FromBody] GetGamesQuery query) => await _mediator.Send(query);

    [HttpPost]
    [Route("GetGame")]
    public async Task<Response<GameDto>> GetGame([FromBody] GetGameQuery query) => await _mediator.Send(query);

    [Authorize]
    [HttpPost]
    [Route("Create")]
    public async Task<Response<GameDto>> CreateGame([FromBody] JoinGameCommand command)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        command.UserGuid = user.Guid;
        return await _mediator.Send(command);
    }

    [Authorize]
    [HttpPost]
    [Route("Join")]
    public async Task<Response<GameDto>> JoinGame([FromBody] JoinGameCommand command)
    {
        var user = await _userManager.FindByNameAsync(User.Identity.Name);
        command.UserGuid = user.Guid;

        return await _mediator.Send(command);
    }

    [Authorize]
    [HttpPost]
    [Route("Leave")]
    public async Task<Response<GameDto>> LeaveGame([FromBody] LeaveGameCommand command)
    {
        return await _mediator.Send(command);
    }

    [Authorize]
    [HttpPost]
    [Route("Ready")]
    public async Task<Response<RoundReadyDto>> Ready([FromBody] RoundReadyCommand command)
    {
        return await _mediator.Send(command);
    }

}
