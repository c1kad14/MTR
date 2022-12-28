using MTR.Domain;

namespace MTR.Core.Abstractions;

public interface IRoundManager
{
    Round RoundInit(Game game, List<Card> cards, Guid roundGuid);
}
