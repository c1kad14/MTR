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

    public Round RoundInit(Game game, List<Card> cards, Guid roundGuid)
    {
        var players = game.Players.Where(p => !p.Removed.Any()).ToList();
        var suit = _cardsManager.GetNextRoundSuit(game.Rounds);
        var round = new Round { Guid = roundGuid, GameId = game.Id, Suit = suit, Sequence = game.Rounds.Count() + 1 };
        var lastRound = game.Rounds.OrderByDescending(r => r.Sequence).FirstOrDefault();
        var defaultStartPosition = 1;

        if (lastRound != null)
        {
            var lastRoundLooser = lastRound.RoundResults.OrderBy(r => r.Score).First();
            defaultStartPosition = lastRoundLooser.Player.Position;
        }

        var roundCards = _cardsManager.GenerateRoundCards(round, cards, players);
        round.StartPosition = round.StartPosition == -1 ? defaultStartPosition : round.StartPosition;

        return round;
    }
}
