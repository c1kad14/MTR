using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using MTR.API.Commands;
using MTR.API.Models;
using MTR.Core.Abstractions;
using MTR.DAL;
using MTR.DTO;

namespace MTR.API.Handlers;

public class TossCardsCommandHandler : IRequestHandler<TossCardsCommand, Response<ActionDto>>
{
    private readonly MTRContext _context;
    private readonly IMapper _mapper;
    private readonly IActionManager _actionManager;

    public TossCardsCommandHandler(MTRContext context, IMapper mapper, IActionManager actionManager)
    {
        _context = context;
        _mapper = mapper;
        _actionManager = actionManager;
    }


    public async Task<Response<ActionDto>> Handle(TossCardsCommand request, CancellationToken cancellationToken)
    {
        var round = await _context.Rounds.Include(r => r.RoundCards)
                                         .ThenInclude(rc => rc.PlayerCards)
                                         .ThenInclude(pc => pc.Player)
                                         .ThenInclude(p => p.Position)
                                         .FirstOrDefaultAsync(rc => rc.Guid == request.RoundGuid);

        if (round is null)
        {
            return new Response<ActionDto> { Message = "Round is invalid." };
        }

        var turn = await _context.Turns
            .Include(t => t.Round)
            .Where(t => t.Round.Guid == request.RoundGuid)
            .OrderByDescending(t => t.Modified)
            .FirstOrDefaultAsync();

        if (turn is null)
        {
            return new Response<ActionDto> { Message = "Turn is invalid." };
        }

        var player = await _context.Players.FirstOrDefaultAsync(p => !p.Removed.Any() && p.Guid == request.PlayerGuid);

        if (player is null)
        {
            return new Response<ActionDto> { Message = "Player is invalid" };
        }

        var action = await _context.Actions.FirstOrDefaultAsync(a => a.Guid == request.Guid);

        if (action is null)
        {
            var roundCards = await _context.RoundCards
                .Include(rc => rc.Card)
                .Include(rc => rc.Round)
                .Where(rc => request.Cards.Any(c => c.Guid == rc.Guid))
                .ToListAsync();

            var canToss = _actionManager.CanToss(round, turn, player, roundCards);

            if (!canToss)
            {
                return new Response<ActionDto> { Message = "Can't toss a card" };
            }

            action = _actionManager.TossCards(turn, player, roundCards);

            _context.Actions.Add(action);
            await _context.SaveChangesAsync();
        }

        var actionDto = _mapper.Map<ActionDto>(action);

        return new Response<ActionDto>
        {
            Success = true,
            Model = actionDto
        };
    }
}
