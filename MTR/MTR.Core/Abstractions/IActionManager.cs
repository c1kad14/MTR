using MTR.Domain;

using Action = MTR.Domain.Action;

namespace MTR.Core.Abstractions;

public interface IActionManager
{
    Action TossCards(Turn turn, Player player, List<RoundCard> roundCards);
    Action HitCards(Turn turn, List<TurnCard> turnCards);
    Action SkipTurn(Turn turn, Player player);
    Action TakeCards(Turn turn);
    Action MuckCards(Turn turn, Player player);
    bool CanToss(Round round, Turn turn, Player player, List<RoundCard> roundCards);
}
