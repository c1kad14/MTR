using MTR.Domain;

namespace MTR.Core.Abstractions;

public interface IRoundManager
{
    Round RoundInit(Game game, List<Card> cards);
    RoundResult GetNextRoundResult(Round round, Player player, int playerCount, int penalty = 0);
}
