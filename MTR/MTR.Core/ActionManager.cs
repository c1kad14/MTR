using MTR.Core.Abstractions;
using MTR.Domain;

using Action = MTR.Domain.Action;

namespace MTR.Core;

public class ActionManager : IActionManager
{
    private readonly IPlayerManager _playerManager;

    public ActionManager(IPlayerManager playerManager)
    {
        _playerManager = playerManager;
    }

    public Domain.Action HitCards(Turn turn, List<TurnCard> turnCards)
    {
        throw new NotImplementedException();
    }

    public Domain.Action MuckCards(Turn turn, Player player)
    {
        throw new NotImplementedException();
    }

    public Domain.Action SkipTurn(Turn turn, Player player)
    {
        throw new NotImplementedException();
    }

    public Domain.Action TakeCards(Turn turn)
    {
        throw new NotImplementedException();
    }

    public Domain.Action TossCards(Turn turn, Player player, List<RoundCard> roundCards)
    {
        var turnCards = new List<TurnCard>();

        foreach (var roundCard in roundCards)
        {
            var turnCard = new TurnCard
            {
                RoundCard = roundCard,
                RoundCardId = roundCard.Id
            };
            turnCards.Add(turnCard);
        }

        return new Action
        {
            Turn = turn,
            TurnId = turn.Id,
            Player = player,
            PlayerId = player.Id,
            ActionType = ActionType.TOSS,
            TurnCards = turnCards
        };
    }

    public bool CanToss(Round round, Turn turn, Player player, List<RoundCard> roundCards)
    {
        var players = _playerManager.GetPlayersWithCards(round);
        var nextPlayer = _playerManager.GetNextPlayer(players, turn.OppositePlayer.Position.Single().Position);
        var turnCardIds = turn.TurnCards.GroupBy(tc => tc.RoundCardId).Select(tcg => tcg.Key).ToList();

        if (turn.PlayerId == player.Id)
        {
            if (!turn.Actions.Any() && roundCards.GroupBy(rc => rc.Id).Count() == 1)
            {
                return true;
            }

            if (roundCards.All(rc => turnCardIds.Contains(rc.Id)))
            {
                return true;
            }

            return false;
        }

        if (turn.PlayerId == player.Id)
        {
            if (roundCards.All(rc => turnCardIds.Contains(rc.Id)))
            {
                return true;
            }

            return false;
        }

        if (turn.OppositePlayerId == player.Id)
        {
            return false;
        }

        var firstCard = turn.Actions.OrderBy(a => a.Modified).First().TurnCards.First();

        if (roundCards.All(rc => rc.Id == firstCard.RoundCardId))
        {
            return true;
        }

        return false;
    }
}
