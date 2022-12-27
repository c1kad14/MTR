using MTR.Core.Abstractions;
using MTR.Domain;

namespace MTR.Core;

public class CardsManager : ICardsManager
{
    public CardsManager()
    {

    }

    public Suit GetNextRoundSuit(List<Round> rounds)
    {
        var lastRound = rounds.OrderBy(r => r.Ended).LastOrDefault();
        var lastRoundSuit = (int)((lastRound?.Suit ?? 0) + 1);
        return lastRoundSuit > 4 ? Suit.SPADES : (Suit)lastRoundSuit;
    }
}
