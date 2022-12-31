using MTR.Domain;

namespace MTR.Core.Abstractions;

public interface ICardManager
{
    List<RoundCard> GenerateRoundCards(Round round, List<Card> cards, List<Player> players);
    Suit GetNextRoundSuit(List<Round> rounds);
}