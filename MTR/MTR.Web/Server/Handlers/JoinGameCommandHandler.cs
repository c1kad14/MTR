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

public record JoinGameCommandHandler : IRequestHandler<JoinGameCommand, Response<GameDto>>
{
    private readonly MTRContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<MTRUser> _userManager;

    public JoinGameCommandHandler(MTRContext context, IMapper mapper, UserManager<MTRUser> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<Response<GameDto>> Handle(JoinGameCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            var user = await _context.Users.Include(u => u.Details).SingleOrDefaultAsync(u => u.Guid == request.UserGuid);

            if (user is null)
            {
                return new Response<GameDto> { Message = "Invalid user." };
            }

            var game = await _context.Games
                .Include(g => g.Rounds)
                .Include(g => g.Players)
                .ThenInclude(p => p.Removed)
                .Include(g => g.Players)
                .ThenInclude(p => p.MTRUser)
                .SingleOrDefaultAsync(g => g.Guid == request.Guid);

            if (game is null)
            {
                game = _mapper.Map<Game>(request);
                game.Status.Add(new()
                {
                    Status = StatusType.NotStarted
                });
                game.Rounds.Add(new()
                {
                    Guid = request.Guid,
                    Sequence = 1,
                    Suit = Suit.SPADES,
                    Status = new()
                    {
                        new RoundStatus
                        {
                            Status = StatusType.NotStarted
                        }
                    }
                });

                await _context.Games.AddAsync(game);
                await _context.SaveChangesAsync();
            }

            if (game.Status.Any(s => s.Status != StatusType.NotStarted))
            {
                return new Response<GameDto> { Message = "Game has already started." };
            }

            if (game.Players.Count(p => !p.Removed.Any()) >= (int)game.TableType)
            {
                return new Response<GameDto> { Message = "Number of players reached the cap." };
            }

            var player = game.Players.SingleOrDefault(p => p.MTRUser.Guid == request.UserGuid && !p.Removed.Any());

            if (player is null)
            {
                player = _mapper.Map<Player>((request, game, user));
                await _context.Players.AddAsync(player);
                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();

            var playerUserIds = game.Players.Where(p => !p.Removed.Any()).Select(p => p.MTRUserId).ToList();
            var userDetails = await _context.UserDetails
                .Include(p => p.MTRUser)
                .Where(d => playerUserIds.Contains(d.MTRUserId))
                .GroupBy(d => d.MTRUserId)
                .Select(d => d.OrderByDescending(d => d.Modified).First())
                .ToListAsync();

            var gameDto = _mapper.Map<GameDto>((game, userDetails));

            return new Response<GameDto> { Success = true, Model = gameDto };
        }
        catch (Exception ex)
        {
            return new Response<GameDto> { Message = ex.Message };
        }
    }
}
