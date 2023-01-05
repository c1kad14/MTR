using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using MTR.Web.Shared.Commands;
using MTR.Web.Shared.Models;
using MTR.Core.Abstractions;
using MTR.DAL;
using MTR.Domain;
using MTR.DTO;

namespace MTR.Web.Shared.Handlers;

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
        else if (game.Status.Any(s => s.Status == StatusType.Completed))
        {
            return new Response<RoundDto> { Message = "Game is over." };
        }
        else if (game.Rounds.Any(r => r.Guid == request.RoundGuid))
        {
            return new Response<RoundDto> { Message = "Round exists." };
        }
        else if (game.Rounds.OrderBy(r => r.Sequence).Last().Status.OrderBy(s => s.Modified).Last().Status == StatusType.Completed)
        {
            return new Response<RoundDto> { Message = "Current round is not finished." };
        }
        else if (game.Rounds.Count() > 11)
        {
            return new Response<RoundDto> { Message = "Round count exceeds maximum." };
        }
        else if (game.Players.Where(p => !p.Removed.Any()).Count() < 3)
        {
            return new Response<RoundDto> { Message = "At least 3 players required." };
        }

        var cards = await _context.Cards.ToListAsync();
        var round = _roundManager.RoundInit(game, cards);
        round.Guid = request.RoundGuid;

        _context.Rounds.Add(round);
        await _context.SaveChangesAsync();

        var roundDto = _mapper.Map<RoundDto>(round);

        return new Response<RoundDto> { Model = roundDto };
    }
}
