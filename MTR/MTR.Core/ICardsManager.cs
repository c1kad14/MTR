using MTR.Domain;

namespace MTR.Core.Abstractions;

public interface ICardsManager
{
    Suit GetNextRoundSuit(List<Round> rounds);
}