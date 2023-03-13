using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using MTR.DAL;
using MTR.Domain;
using MTR.DTO;
using MTR.Web.Shared.Commands;
using MTR.Web.Shared.Models;

namespace MTR.Web.Server.Handlers;

public class SitPlayerCommandHandler : IRequestHandler<SitPlayerCommand, Response<SitPlayerDto>>
{
    private readonly IMapper _mapper;
    private readonly MTRContext _context;

    public SitPlayerCommandHandler(IMapper mapper, MTRContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Response<SitPlayerDto>> Handle(SitPlayerCommand request, CancellationToken cancellationToken)
    {
        if (request.Guid == default)
        {
            return new Response<SitPlayerDto> { Message = "Player position id is invalid." };
        }

        if (request.PlayerGuid == default)
        {
            return new Response<SitPlayerDto> { Message = "Player id is invalid." };
        }

        if (request.Position == default)
        {
            return new Response<SitPlayerDto> { Message = "Position is invalid" };
        }

        var player = await _context.Players.SingleOrDefaultAsync(p => p.Guid == request.PlayerGuid);

        if (player is null)
        {
            return new Response<SitPlayerDto> { Message = "Player is invalid." };
        }

        var playerPosition = await _context.PlayerPositions.SingleOrDefaultAsync(pp => pp.Guid == request.Guid);

        if (playerPosition is not null)
        {
            return new Response<SitPlayerDto> { Model = _mapper.Map<SitPlayerDto>((player, playerPosition)), Success = true };
        }

        var players = await _context.Players
            .Include(p => p.Position)
            .Where(p => p.GameId == player.GameId)
            .ToListAsync();

        var isPostionEmpty = players.All(p => p.Position.All(pp => pp.Position != request.Position));

        if (!isPostionEmpty)
        {
            return new Response<SitPlayerDto> { Message = $"Position {request.Position} is not empty" };
        }

        playerPosition = _mapper.Map<PlayerPosition>((player, request));

        await _context.PlayerPositions.AddAsync(playerPosition);
        await _context.SaveChangesAsync();

        return new Response<SitPlayerDto> { Model = _mapper.Map<SitPlayerDto>(playerPosition), Success = true };
    }
}
