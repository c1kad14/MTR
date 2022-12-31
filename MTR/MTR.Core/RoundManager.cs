using MTR.Core.Abstractions;
using MTR.DAL;
using MTR.Domain;

namespace MTR.Core;

public class RoundManager : IRoundManager
{
    private readonly ICardManager _cardManager;

    public RoundManager(ICardManager cardsManager)
    {
        _cardManager = cardsManager;
    }

    public Round RoundInit(Game game, List<Card> cards)
    {
        var players = game.Players.Where(p => !p.Removed.Any()).ToList();
        var suit = _cardManager.GetNextRoundSuit(game.Rounds);
        var round = new Round { Game = game, GameId = game.Id, Suit = suit, Sequence = game.Rounds.Count() + 1 };
        var roundCards = _cardManager.GenerateRoundCards(round, cards, players);

        if (round.StartPosition == default)
        {
            var lastRound = game.Rounds.OrderByDescending(r => r.Sequence).FirstOrDefault();
            var lastRoundLooser = lastRound?.RoundResults.OrderBy(r => r.Score).First();
            round.StartPosition = lastRoundLooser?.Player.Position.Single().Position ?? 1;
        }

        round.RoundCards = roundCards;

        return round;
    }

    public RoundResult GetNextRoundResult(Round round, Player player, int playerCount, int penalty = 0)
    {
        var previousRoundResult = round.RoundResults.OrderBy(r => r.Score).FirstOrDefault();

        if (previousRoundResult == default)
        {
            return new RoundResult { Player = player, PlayerId = player.Id, Score = playerCount, Round = round, RoundId = round.Id };
        }

        var score = previousRoundResult.Score - 1 + penalty;

        var roundResult = new RoundResult
        {
            Player = player,
            PlayerId = player.Id,
            Score = score,
            Round = round,
            RoundId = round.Id
        };

        return roundResult;
    }

}
