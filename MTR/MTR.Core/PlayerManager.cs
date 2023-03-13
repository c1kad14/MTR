using MTR.Core.Abstractions;
using MTR.Domain;

using System.Text;

namespace MTR.Core;

public class PlayerManager : IPlayerManager
{
    public List<Player> GetPlayersWithCards(Round round)
    {
        return round.RoundCards
            .Where(rc => rc.PlayerCards.Any() && !rc.MuckedCards.Any()) // that players hold
            .SelectMany(rc => rc.PlayerCards) // get player cards
            .GroupBy(pc => pc.RoundCardId) // group cards
            .Select(pcg => pcg.OrderByDescending(pc => pc.Modified).First()) // get last owner
            .Select(pc => pc.Player) // get players
            .DistinctBy(p => p.Id) // get unique
            .ToList();
    }

    public Player GetNextPlayer(List<Player> players, int previousPosition)
    {
        var playersOrderedByPosition = players.OrderBy(p => p.Position.Single().Position).ToList();
        return playersOrderedByPosition.FirstOrDefault(p => p.Position.Any(p => p.Position > previousPosition))
            ?? playersOrderedByPosition.First();
    }
}
