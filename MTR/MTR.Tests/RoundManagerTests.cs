using Autofac;
using Autofac.Extras.Moq;

using Moq;

using MTR.Core;
using MTR.Core.Abstractions;
using MTR.Domain;

namespace MTR.Tests;

public class RoundManagerTests
{
    [Fact]
    public void RoundInit_FirstRound_Success()
    {
        var am = AutoMock.GetStrict(x => x.RegisterType<CardManager>().As<ICardManager>());
        var manager = am.Create<RoundManager>();

        var game = new Game { Players = WithPlayers() };
        var cards = WithCards();
        var round = manager.RoundInit(game, cards);

        Assert.NotNull(round);
        Assert.Equal(24, round.RoundCards.Where(r => r.PlayerCards.Any()).Count());
        Assert.Equal(1, round.Sequence);
        Assert.Equal(Suit.SPADES, round.Suit);
        Assert.True(round.StartPosition > 0);
    }

    [Fact]
    public void RoundInit_SecondRound_Success()
    {
        var am = AutoMock.GetStrict(x => x.RegisterType<CardManager>().As<ICardManager>());
        var manager = am.Create<RoundManager>();

        var game = new Game { Players = WithPlayers(), Rounds = WithRounds() };
        var cards = WithCards();
        var round = manager.RoundInit(game, cards);

        Assert.NotNull(round);
        Assert.Equal(24, round.RoundCards.Where(r => r.PlayerCards.Any()).Count());
        Assert.Equal(Suit.HEARTS, round.Suit);
        Assert.Equal(2, round.Sequence);
        Assert.True(round.StartPosition > 0);
    }

    [Fact]
    public void RoundInit_SecondRoundNoStartPosition_SixthPlayerStartPosition()
    {
        var am = AutoMock.GetLoose();
        //am.Mock<ICardsManager>()
        //    .Setup(x => x.GenerateRoundCards(It.IsAny<Round>(), It.IsAny<List<Card>>(), It.IsAny<List<Player>>()))
        //    .Returns();
        var manager = am.Create<RoundManager>();

        var game = new Game { Players = WithPlayers(), Rounds = WithRounds() };
        var cards = WithCards();
        var round = manager.RoundInit(game, cards);

        Assert.NotNull(round);
        Assert.Equal(2, round.Sequence);
        Assert.Equal(2, round.StartPosition);
    }

    [Fact]
    public void RoundInit_FirstRoundNoStartPosition_FirstPlayerStartPosition()
    {
        var am = AutoMock.GetLoose();
        //am.Mock<ICardsManager>()
        //    .Setup(x => x.GenerateRoundCards(It.IsAny<Round>(), It.IsAny<List<Card>>(), It.IsAny<List<Player>>()))
        //    .Returns();
        var manager = am.Create<RoundManager>();

        var game = new Game { Players = WithPlayers() };
        var cards = WithCards();
        var round = manager.RoundInit(game, cards);

        Assert.NotNull(round);
        Assert.Equal(1, round.Sequence);
        Assert.Equal(1, round.StartPosition);
    }

    private List<Round> WithRounds() =>
        new()
        {
            new Round
            {
                Suit = Suit.SPADES,
                Sequence = 1,
                RoundResults = new()
                {
                    new RoundResult
                    {
                        PlayerId = 3,
                        Score = 4
                    },
                    new RoundResult
                    {
                        PlayerId = 4,
                        Score = 3
                    },
                    new RoundResult
                    {
                        PlayerId = 5,
                        Score = 2
                    },
                    new RoundResult
                    {
                        PlayerId = 6,
                        Score = 0,
                        Player = new() { Position = new(){ new() { Position = 2 } } }
                    },
                }
            },
        };

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
                new Player { Id = 1, Position = new(){ new() { Position = 1 } }, Removed = new() { new() } },
                new Player { Id = 2, Position = new(){ new() { Position = 2 } }, Removed = new() { new() } },
                new Player { Id = 3, Position = new(){ new() { Position = 3 } } },
                new Player { Id = 4, Position = new(){ new() { Position = 4 } } },
                new Player { Id = 5, Position = new(){ new() { Position = 5 } } },
                new Player { Id = 6, Position = new(){ new() { Position = 6 } } },
        };


}
