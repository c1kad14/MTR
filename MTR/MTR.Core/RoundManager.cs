using MTR.Core.Abstractions;
using MTR.DAL;
using MTR.Domain;

namespace MTR.Core;

public class RoundManager : IRoundManager
{
    private readonly MTRContext _context;
    private readonly ICardsManager _cardsManager;

    public RoundManager(MTRContext context, ICardsManager cardsManager)
    {
        _context = context;
        _cardsManager = cardsManager;
    }

    public async Task<Round> BeginRoundAsync(Game game, Guid roundGuid)
    {
        var suit = _cardsManager.GetNextRoundSuit(game.Rounds);
        var round = new Round { Guid = roundGuid, Game = game, Suit = suit, Sequence = game.Rounds.Count() + 1 };


        return await Task.FromResult(round);
    }
}
