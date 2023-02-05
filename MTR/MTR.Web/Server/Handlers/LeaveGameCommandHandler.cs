using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using MTR.Web.Shared.Commands;
using MTR.Web.Shared.Models;
using MTR.DAL;
using MTR.Domain;
using MTR.DTO;
using Microsoft.AspNetCore.Identity;

namespace MTR.Web.Shared.Handlers;

public record LeaveGameCommandHandler : IRequestHandler<LeaveGameCommand, Response<GameDto>>
{
    private readonly MTRContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<MTRUser> _userManager;

    public LeaveGameCommandHandler(MTRContext context, IMapper mapper, UserManager<MTRUser> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<Response<GameDto>> Handle(LeaveGameCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var game = await _context.Games
                .Include(g => g.Rounds)
                .Include(g => g.Players)
                .ThenInclude(p => p.Removed)
                .Include(g => g.Players)
                .ThenInclude(p => p.MTRUser)
                .SingleOrDefaultAsync(g => g.Guid == request.Guid);

            if (game is null)
            {
                return new Response<GameDto> { Message = "Game does not exist." };
            }

            if (game.Status.Any(s => s.Status != StatusType.NotStarted))
            {
                return new Response<GameDto> { Message = "Game has already started." };
            }

            var player = game.Players.SingleOrDefault(p => p.Guid == request.PlayerGuid && !p.Removed.Any());

            if (player is null)
            {
                return new Response<GameDto> { Message = "Invalid player." };
            }

            _context.PlayerRemoved.Add(new() { PlayerId = player.Id });
            await _context.SaveChangesAsync();

            var gameDto = _mapper.Map<GameDto>(game);

            return new Response<GameDto> { Success = true, Model = gameDto };
        }
        catch (Exception ex)
        {
            return new Response<GameDto> { Message = ex.Message };
        }
    }
}
