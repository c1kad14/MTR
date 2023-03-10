using MTR.Core.Abstractions;
using MTR.Domain;

namespace MTR.Core;

public class CardManager : ICardManager
{
    public List<RoundCard> GenerateRoundCards(Round round, List<Card> cards, List<Player> players)
    {
        var random = new Random();
        var roundCards = new List<RoundCard>();
        RoundCard? minRankSuitCard = null;

        foreach (var card in cards)
        {
            var roundCard = new RoundCard { Round = round, Card = card, CardId = card.Id };
            roundCards.Add(roundCard);
        }

        foreach (var player in players)
        {
            for (var i = 0; i < 6; i++)
            {
                var availableCards = roundCards.Where(c => !c.PlayerCards.Any()).ToList();
                var cardIndex = random.Next(0, availableCards.Count - 1);
                var card = availableCards[cardIndex];
                minRankSuitCard = card.Card.Suit == round.Suit && (card.Card.Rank <= (minRankSuitCard?.Card.Rank ?? Rank.ACE)) ? card : minRankSuitCard;

                card.PlayerCards.Add(new PlayerCard { RoundCard = card, Player = player, PlayerId = player.Id });
            }
        }

        if (minRankSuitCard?.PlayerCards.Single().Player is not null)
        {
            round.StartPlayer.Add(new()
            {
                Player = minRankSuitCard.PlayerCards.Single().Player,
                PlayerId = minRankSuitCard.PlayerCards.Single().PlayerId
            });
        }

        return roundCards;
    }

    public Suit GetNextRoundSuit(List<Round> rounds)
    {
        var lastRound = rounds.MaxBy(r => r.Started);
        var lastRoundSuit = (int)((lastRound?.Suit ?? 0) + 1);
        return lastRoundSuit > 4 ? Suit.SPADES : (Suit)lastRoundSuit;
    }
}
