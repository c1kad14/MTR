using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using MTR.API.Commands;
using MTR.API.Models;
using MTR.Core.Abstractions;
using MTR.DAL;
using MTR.Domain;
using MTR.DTO;

namespace MTR.API.Handlers;

public class BeginRoundCommandHandler : IRequestHandler<BeginRoundCommand, Response<RoundDto>>
{
    private MTRContext _context;
    private readonly IMapper _mapper;
    private readonly IRoundManager _roundManager;

    public BeginRoundCommandHandler(MTRContext context, IMapper mapper, IRoundManager roundManager)
    {
        _context = context;
        _mapper = mapper;
        _roundManager = roundManager;
    }

    public async Task<Response<RoundDto>> Handle(BeginRoundCommand request, CancellationToken cancellationToken)
    {
        var game = await _context.Games.Include(g => g.Rounds).SingleOrDefaultAsync(g => g.Guid == request.GameGuid);

        if (game is null)
        {
            return new Response<RoundDto> { Message = "Game does not exist." };
        }
        else if (game.Ended is not null)
        {
            return new Response<RoundDto> { Message = "Game is over." };
        }
        else if (game.Rounds.Any(r => r.Guid == request.RoundGuid))
        {
            return new Response<RoundDto> { Message = "Round exists." };
        }
        else if (game.Rounds.Any(r => r.Ended is not null))
        {
            return new Response<RoundDto> { Message = "Current round is not finished." };
        }
        else if (game.Rounds.Count() > 11)
        {
            return new Response<RoundDto> { Message = "Round count exceeds maximum." };
        }

        var cards = await _context.Cards.ToListAsync();
        var round = _roundManager.RoundInit(game, cards, request.RoundGuid);

        return null;
    }
}
