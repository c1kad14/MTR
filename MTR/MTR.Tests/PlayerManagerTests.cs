using Autofac.Extras.Moq;

using MTR.Core;
using MTR.Domain;

namespace MTR.Tests;

public class PlayerManagerTests
{
    [Fact]
    public void GetPlayerWithCards_NoMuck_TwoPlayers()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<PlayerManager>();
        var cards = WithCards();
        var round = new Round
        {
            RoundCards = WithRoundCardsNoMuck()
        };

        var result = manager.GetPlayersWithCards(round);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetPlayerWithCards_WithMuck_ThreePlayers()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<PlayerManager>();
        var cards = WithCards();
        var round = new Round
        {
            RoundCards = WithRoundCardsWithMuck()
        };

        var result = manager.GetPlayersWithCards(round);

        Assert.NotNull(result);
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void GetNextPlayer_PreviousPositionIsFirst_ReturnSecond()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<PlayerManager>();
        var players = WithPlayersNoOut();

        var result = manager.GetNextPlayer(players, 1);

        Assert.Equal(2, result.Id);
    }

    [Fact]
    public void GetNextPlayer_PreviousPositionIsLast_ReturnFirst()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<PlayerManager>();
        var players = WithPlayersNoOut();

        var result = manager.GetNextPlayer(players, 4);

        Assert.Equal(1, result.Id);
    }

    [Fact]
    public void GetNextPlayer_NextPositionIsOut_ReturnNext()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<PlayerManager>();
        var players = WithPlayersOneOut();

        var result = manager.GetNextPlayer(players, 1);

        Assert.Equal(3, result.Id);
    }

    [Fact]
    public void GetNextPlayer_NextPositionIsOutNoNext_ReturnFirst()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<PlayerManager>();
        var players = WithPlayersOneOut();

        var result = manager.GetNextPlayer(players, 3);

        Assert.Equal(1, result.Id);
    }

    private List<Player> WithPlayersNoOut()
    {
        return new List<Player>
        {
            new ()
            {
                Id = 1,
                Position = new()
                {
                    new() { Position = 1 }
                }
            },
            new ()
            {
                Id = 2,
                Position = new()
                {
                    new() { Position = 2 }
                }
            },
            new ()
            {
                Id = 3,
                Position = new()
                {
                    new() { Position = 3 }
                }
            },
            new ()
            {
                Id = 4,
                Position = new()
                {
                    new() { Position = 4 }
                }
            },
        };
    }

    private List<Player> WithPlayersOneOut()
    {
        return new List<Player>
        {
            new ()
            {
                Id = 1,
                Position = new()
                {
                    new() { Position = 1 }
                }
            },
            new ()
            {
                Id = 3,
                Position = new()
                {
                    new() { Position = 3 }
                }
            }
        };
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

    private List<RoundCard> WithRoundCardsNoMuck()
    {
        var modified = DateTime.UtcNow;
        return new List<RoundCard>()
        {
            new()
            {
                CardId = 1,
                Card = new()
                {
                    Id = 1
                },
                Modified = modified.AddMinutes(-1)
            },
            new()
            {
                CardId = 2,
                Card = new()
                {
                    Id = 2
                },
                Modified = modified.AddMinutes(-1),
                PlayerCards = new()
                {
                    new()
                    {
                        RoundCardId = 2,
                        PlayerId = 1,
                        Player = new()
                        {
                           Id = 1
                        }
                    }
                }
            },
            new()
            {
                CardId = 3,
                Card = new()
                {
                    Id = 3
                },
                Modified = modified.AddMinutes(-1),
                PlayerCards = new()
                {
                    new()
                    {
                        RoundCardId = 3,
                        PlayerId = 1,
                        Player = new()
                        {
                           Id = 1
                        }
                    }
                }
            },
            new()
            {
                CardId = 4,
                Card = new()
                {
                    Id = 4
                },
                Modified = modified.AddMinutes(-1),
                PlayerCards = new()
                {
                    new()
                    {
                        RoundCardId = 4,
                        PlayerId = 2,
                        Player = new()
                        {
                           Id = 2
                        }
                    }
                }
            },
            new()
            {
                CardId = 5,
                Card = new()
                {
                    Id = 5
                },
                Modified = modified.AddMinutes(-1),
                PlayerCards = new()
                {
                    new()
                    {
                        RoundCardId = 5,
                        PlayerId = 2,
                        Player = new()
                        {
                           Id = 2
                        }
                    }
                }
            }
        };
    }

    private List<RoundCard> WithRoundCardsWithMuck()
    {
        var modified = DateTime.UtcNow;
        return new List<RoundCard>()
        {
            new()
            {
                CardId = 1,
                Card = new()
                {
                    Id = 1
                },
            },
            new()
            {
                CardId = 2,
                Card = new()
                {
                    Id = 2
                },
                MuckedCards = new()
                {
                    new()
                    {
                        RoundCardId = 2
                    }
                }
            },
            new()
            {
                CardId = 11,
                Card = new()
                {
                    Id = 11
                },
                MuckedCards = new()
                {
                    new()
                    {
                        RoundCardId = 11
                    }
                }
            },
            new()
            {
                CardId = 10,
                Card = new()
                {
                    Id = 10
                },
                PlayerCards = new()
                {
                    new()
                    {
                        Modified = modified.AddMinutes(-1),
                        RoundCardId = 10,
                        PlayerId = 4,
                        Player = new()
                        {
                           Id = 4
                        }
                    }
                },
                MuckedCards = new()
                {
                    new()
                    {
                        RoundCardId = 10
                    }
                }
            },
            new()
            {
                CardId = 8,
                Card = new()
                {
                    Id = 8
                },
                PlayerCards = new()
                {
                    new()
                    {
                        Modified = modified.AddMinutes(-2),
                        RoundCardId = 8,
                        PlayerId = 5,
                        Player = new()
                        {
                           Id = 5
                        }
                    }
                }
            },
            new()
            {
                CardId = 8,
                Card = new()
                {
                    Id = 8
                },
                PlayerCards = new()
                {
                    new()
                    {
                        Modified = modified.AddMinutes(-2),
                        RoundCardId = 8,
                        PlayerId = 4,
                        Player = new()
                        {
                           Id = 4
                        }
                    }
                }
            },
            new()
            {
                CardId = 3,
                Card = new()
                {
                    Id = 3
                },
                PlayerCards = new()
                {
                    new()
                    {
                        Modified = modified.AddMinutes(-1),
                        RoundCardId = 3,
                        PlayerId = 1,
                        Player = new()
                        {
                           Id = 1
                        }
                    }
                }
            },
            new()
            {
                CardId = 4,
                Card = new()
                {
                    Id = 4
                },
                PlayerCards = new()
                {
                    new()
                    {
                        Modified = modified.AddMinutes(-1),
                        RoundCardId = 4,
                        PlayerId = 2,
                        Player = new()
                        {
                           Id = 2
                        }
                    }
                }
            },
            new()
            {
                CardId = 5,
                Card = new()
                {
                    Id = 5
                },
                PlayerCards = new()
                {
                    new()
                    {
                        Modified = modified.AddMinutes(-1),
                        RoundCardId = 5,
                        PlayerId = 2,
                        Player = new()
                        {
                           Id = 2
                        }
                    }
                }
            },
            new()
            {
                CardId = 8,
                Card = new()
                {
                    Id = 8
                },
                PlayerCards = new()
                {
                    new()
                    {
                        Modified = modified.AddMinutes(-1),
                        RoundCardId = 8,
                        PlayerId = 3,
                        Player = new()
                        {
                           Id = 3
                        }
                    }
                }
            },
        };
    }
}
