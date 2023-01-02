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

    public Action HitCards(Turn turn, Player player, TurnCard turnCard, RoundCard roundCard)
    {
        return new Action
        {
            ActionType = ActionType.HIT,
            TurnId = turn.Id,
            TurnCards = new()
            {
                new()
                {
                    TurnId = turn.Id,
                    Turn = turn,
                    RoundCardId = roundCard.Id,
                    RoundCard = roundCard,
                    OppositeTurnCardId = turnCard.Id,
                    OppositeTurnCard = turnCard
                }
            },
            PlayerId = player.Id
        };
    }

    public Action MuckCards(Turn turn, Player player)
    {
        var muckedCards = new List<MuckedCard>();
        var hitActions = turn.Actions.Where(a => a.ActionType == ActionType.HIT)
            .ToList();

        foreach (var turnCard in hitActions.SelectMany(a => a.TurnCards))
        {
            muckedCards.AddRange(new List<MuckedCard>
            {
                new()
                {
                    RoundCardId = turnCard.RoundCardId,
                    RoundCard = turnCard.RoundCard
                },
                new()
                {
                    RoundCardId = turnCard.OppositeTurnCard!.RoundCardId,
                    RoundCard = turnCard.OppositeTurnCard!.RoundCard
                },
            });
        }

        return new Action
        {
            ActionType = ActionType.MUCK,
            PlayerId = player.Id,
            Player = player,
            TurnId = turn.Id,
            Turn = turn,
            MuckedCards = muckedCards
        };
    }

    public Action SkipTurn(Turn turn, Player player)
    {
        return new Action
        {
            ActionType = ActionType.SKIP,
            TurnId = turn.Id,
            Turn = turn,
            PlayerId = player.Id,
            Player = player
        };
    }

    public Action TakeCards(Turn turn)
    {
        var roundCardsToTake = turn.TurnCards.Where(tc => tc.OppositeTurnCardId is null).Select(tc => tc.RoundCard);

        foreach (var roundCard in roundCardsToTake)
        {
            var currentOwner = roundCard.PlayerCards.OrderByDescending(pc => pc.Modified).First();

            if (currentOwner.PlayerId == turn.OppositePlayerId)
            {
                continue;
            }

            roundCard.PlayerCards.Add(new()
            {
                PlayerId = turn.OppositePlayerId,
                Player = turn.OppositePlayer,
                RoundCardId = roundCard.Id,
                RoundCard = roundCard
            });
        }

        return new()
        {
            ActionType = ActionType.TAKE,
            PlayerId = turn.OppositePlayerId,
            Player = turn.OppositePlayer,
            Turn = turn,
            TurnId = turn.Id
        };
    }

    public Action TossCards(Turn turn, Player player, List<RoundCard> roundCards)
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
            ActionType = ActionType.TOSS,
            TurnId = turn.Id,
            Turn = turn,
            PlayerId = player.Id,
            Player = player,
            TurnCards = turnCards
        };
    }

    public bool CanHit(Round round, TurnCard cardToHit, RoundCard oppositeCard) =>
        oppositeCard.Card.Rank > cardToHit.RoundCard.Card.Rank && oppositeCard.Card.Suit == cardToHit.RoundCard.Card.Suit
        || oppositeCard.Card.Suit == round.Suit && cardToHit.RoundCard.Card.Suit != round.Suit;

    public bool CanMuck(Turn turn)
    {
        var turnCards = turn.TurnCards.Where(t => t.OppositeTurnCardId is null);
        var oppositeTurnCards = turn.TurnCards.Where(t => t.OppositeTurnCardId is not null);
        return turnCards.All(tc => oppositeTurnCards.Any(c => c.OppositeTurnCardId == tc.Id));
    }

    public bool CanToss(Round round, Turn turn, Player player, List<RoundCard> cardsToToss)
    {
        var players = _playerManager.GetPlayersWithCards(round);
        var nextPlayer = _playerManager.GetNextPlayer(players, turn.OppositePlayer.Position.Single().Position);
        var turnCardRanks = turn.TurnCards.GroupBy(tc => tc.RoundCard.Card.Rank).Select(tcg => tcg.Key).ToList();
        var cardsToHitCount = turn.TurnCards.Count(c => c.OppositeTurnCardId == null);
        var hasMuck = round.RoundCards.Any(rc => rc.MuckedCards.Any());

        if (turn.Actions.Any(a => a.ActionType == ActionType.TAKE))
        {
            return cardsToToss.All(rc => turnCardRanks.Contains(rc.Card.Rank));
        }

        // can't toss if there are 6 or more cards to hit
        if (cardsToHitCount > 5)
        {
            return false;
        }

        // first muck is 5 cards
        if (!hasMuck && cardsToHitCount > 4)
        {
            return false;
        }

        // turn player
        if (turn.PlayerId == player.Id)
        {
            // if this is first action all cards must be the same rank
            if (!turn.Actions.Any() && cardsToToss.GroupBy(rc => rc.Card.Rank).Count() == 1)
            {
                return true;
            }

            // card with the same rank should be on the board
            if (cardsToToss.All(rc => turnCardRanks.Contains(rc.Card.Rank)))
            {
                return true;
            }

            return false;
        }

        // if this is player next to the opposite player
        if (nextPlayer.Id == player.Id)
        {
            // card with the same rank should be on the board
            if (cardsToToss.All(rc => turnCardRanks.Contains(rc.Card.Rank)))
            {
                return true;
            }

            return false;
        }

        // opposite player can't toss a card to himself
        if (turn.OppositePlayerId == player.Id)
        {
            return false;
        }

        // all other player can toss only first rank card
        var firstCard = turn.Actions.OrderBy(a => a.Modified).First().TurnCards.First();

        if (cardsToToss.All(rc => rc.Card.Rank == firstCard.RoundCard.Card.Rank))
        {
            return true;
        }

        return false;
    }
}
