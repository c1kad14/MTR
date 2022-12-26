using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using MTR.API.Commands;
using MTR.API.Models;
using MTR.DAL;
using MTR.Domain;

namespace MTR.API.Handlers;

public record JoinGameCommandHandler : IRequestHandler<JoinGameCommand, Response<GameDto>>
{
    private readonly MTRContext _context;
    private readonly IMapper _mapper;

    public JoinGameCommandHandler(MTRContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<GameDto>> Handle(JoinGameCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.Include(u => u.Details).SingleOrDefaultAsync(u => u.Guid == request.UserGuid);

        if (user is null)
        {
            return new Response<GameDto> { Message = "Invalid user" };
        }

        var game = await _context.Games
            .Include(g => g.Rounds)
            .Include(g => g.Players)
            .ThenInclude(p => p.Removed)
            .Include(g => g.Players)
            .ThenInclude(p => p.User)
            .SingleOrDefaultAsync(g => g.Guid == request.Guid);

        if (game is null)
        {
            game = _mapper.Map<Game>(request);
            await _context.Games.AddAsync(game);
        }

        if (game.Rounds.Any())
        {
            return new Response<GameDto> { Message = "Game has already started." };
        }

        if (game.Players.Count(p => !p.Removed.Any()) > 5)
        {
            return new Response<GameDto> { Message = "Number of players reached the cap." };
        }

        var player = game.Players.SingleOrDefault(p => p.User.Guid == request.UserGuid && !p.Removed.Any());

        if (player is null)
        {
            player = _mapper.Map<Player>((game, user));
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }

        var playerUserIds = game.Players.Where(p => !p.Removed.Any()).Select(p => p.UserId).ToList();
        var userDetails = await _context.UserDetails
            .Include(p => p.User)
            .Where(d => playerUserIds.Contains(d.UserId))
            .GroupBy(d => d.UserId)
            .Select(d => d.OrderByDescending(d => d.Modified).First())
            .ToListAsync();

        var gameDto = _mapper.Map<GameDto>((game, userDetails));

        return new Response<GameDto> { Success = true, Model = gameDto };
    }
}
