using MTR.Core.Abstractions;
using MTR.Domain;

namespace MTR.Core;

public class TurnManager : ITurnManager
{
    private readonly IPlayerManager _playerManager;

    public TurnManager(IPlayerManager playerManager)
    {
        _playerManager = playerManager;
    }

    public Turn GetNextTurn(Round round, List<Player> players)
    {
        var previousTurn = round.Turns.OrderByDescending(t => t.Modified).FirstOrDefault();
        Player player = null;
        Player oppositePlayer = null;

        if (previousTurn is null)
        {
            player = players.Single(p => p.Id == round.StartPlayer.Single().PlayerId);
        }
        else
        {
            var takeAction = previousTurn.Actions.FirstOrDefault(a => a.ActionType == ActionType.TAKE);

            if (takeAction is null)
            {
                player = _playerManager.GetNextPlayer(players, previousTurn.Player.Position.Single().Position);
            }
            else
            {
                player = _playerManager.GetNextPlayer(players, previousTurn.OppositePlayer.Position.Single().Position);
            }
        }

        oppositePlayer = _playerManager.GetNextPlayer(players, player.Position.Single().Position);

        return new Turn { Player = player, OppositePlayer = oppositePlayer, Round = round };
    }
}
