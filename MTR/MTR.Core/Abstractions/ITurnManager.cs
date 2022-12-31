using MTR.Domain;

namespace MTR.Core.Abstractions;

public interface ITurnManager
{
    Turn GetNextTurn(Round round, List<Player> players);

}
