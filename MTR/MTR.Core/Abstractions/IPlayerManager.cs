using MTR.Domain;

namespace MTR.Core.Abstractions;

public interface IPlayerManager
{
    List<Player> GetPlayersWithCards(Round round);

    Player GetNextPlayer(List<Player> players, int previousPosition);
}
