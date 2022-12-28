using Autofac.Extras.Moq;

using Microsoft.EntityFrameworkCore;

using MTR.Core;
using MTR.Core.Abstractions;
using MTR.Domain;

namespace MTR.Tests
{
    public class CardsManagerTests
    {
        [Fact]
        public void GetNextRoundSuit_NoPreviousSuit_ReturnsSpades()
        {
            var am = AutoMock.GetLoose();
            var manager = am.Create<CardsManager>();

            var result = manager.GetNextRoundSuit(new());

            Assert.Equal(Suit.SPADES, result);
        }

        [Fact]
        public void GetNextRoundSuit_PreviousSuitSpades_ReturnsHearts()
        {
            var am = AutoMock.GetLoose();
            var manager = am.Create<CardsManager>();

            var result = manager.GetNextRoundSuit(new() { new Round { Suit = Suit.SPADES } });

            Assert.Equal(Suit.HEARTS, result);
        }

        [Fact]
        public void GetNextRoundSuit_PreviousSuitClubs_ReturnsHearts()
        {
            var am = AutoMock.GetLoose();
            var manager = am.Create<CardsManager>();

            var result = manager.GetNextRoundSuit(new() { new Round { Suit = Suit.CLUBS } });

            Assert.Equal(Suit.SPADES, result);
        }

        [Fact]
        public void GenerateRoundCards_ThreePlayers_Success()
        {
            var am = AutoMock.GetLoose();
            var manager = am.Create<CardsManager>();

            var cards = WithCards();
            var players = WithPlayers().Take(3).ToList();
            var round = new Round { Suit = Suit.HEARTS };

            var result = manager.GenerateRoundCards(round, cards, players);

            Assert.NotNull(result);
            Assert.Equal(36, result.Count());
            Assert.Equal(18, result.Where(r => r.PlayerCards.Any()).Count());
            Assert.Equal(3, result.SelectMany(r => r.PlayerCards).GroupBy(pc => pc.PlayerId).Count());
            Assert.Equal(18, result.SelectMany(r => r.PlayerCards).Select(pc => pc.RoundCard.CardId).Distinct().Count());
        }

        [Fact]
        public void GenerateRoundCards_FourPlayers_Success()
        {
            var am = AutoMock.GetLoose();
            var manager = am.Create<CardsManager>();

            var cards = WithCards();
            var players = WithPlayers().Take(4).ToList();
            var round = new Round { Suit = Suit.HEARTS };

            var result = manager.GenerateRoundCards(round, cards, players);

            Assert.NotNull(result);
            Assert.Equal(36, result.Count());
            Assert.Equal(24, result.Where(r => r.PlayerCards.Any()).Count());
            Assert.Equal(4, result.SelectMany(r => r.PlayerCards).GroupBy(pc => pc.PlayerId).Count());
            Assert.Equal(24, result.SelectMany(r => r.PlayerCards).Select(pc => pc.RoundCard.CardId).Distinct().Count());
        }

        [Fact]
        public void GenerateRoundCards_FivePlayers_Success()
        {
            var am = AutoMock.GetLoose();
            var manager = am.Create<CardsManager>();

            var cards = WithCards();
            var players = WithPlayers().Take(5).ToList();
            var round = new Round { Suit = Suit.HEARTS };

            var result = manager.GenerateRoundCards(round, cards, players);
            var cardIds = result.SelectMany(r => r.PlayerCards).Select(pc => pc.RoundCardId).ToList();

            Assert.NotNull(result);
            Assert.Equal(36, result.Count());
            Assert.Equal(30, result.Where(r => r.PlayerCards.Any()).Count());
            Assert.Equal(5, result.SelectMany(r => r.PlayerCards).GroupBy(pc => pc.PlayerId).Count());
            Assert.Equal(30, result.SelectMany(r => r.PlayerCards).Select(pc => pc.RoundCard.CardId).Distinct().Count());
        }

        [Fact]
        public void GenerateRoundCards_SixPlayers_Success()
        {
            var am = AutoMock.GetLoose();
            var manager = am.Create<CardsManager>();

            var cards = WithCards();
            var players = WithPlayers();
            var round = new Round { Suit = Suit.HEARTS };

            var result = manager.GenerateRoundCards(round, cards, players);

            Assert.NotNull(result);
            Assert.Equal(36, result.Count());
            Assert.Equal(36, result.Where(r => r.PlayerCards.Any()).Count());
            Assert.Equal(6, result.SelectMany(r => r.PlayerCards).GroupBy(pc => pc.PlayerId).Count());
            Assert.Equal(36, result.SelectMany(r => r.PlayerCards).Select(pc => pc.RoundCard.CardId).Distinct().Count());
        }

        private List<Card> WithCards()
        {
            var id = 0;
            var cards = new List<Card>();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card { Id = ++id, Rank = rank, Suit = suit });
                }
            }

            return cards;
        }

        private List<Player> WithPlayers() =>
            new List<Player>
            {
                new Player { Id = 1, Position = 1 },
                new Player { Id = 2, Position = 2 },
                new Player { Id = 3, Position = 3 },
                new Player { Id = 4, Position = 4 },
                new Player { Id = 5, Position = 5 },
                new Player { Id = 6, Position = 6 },
            };

    }
}