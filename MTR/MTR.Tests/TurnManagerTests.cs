using Autofac;
using Autofac.Extras.Moq;

using MTR.Core;
using MTR.Core.Abstractions;
using MTR.Domain;

namespace MTR.Tests;

public class TurnManagerTests
{
    [Fact]
    public void GetNextTurn_NoPreviousTurnsSecondPositionIsStart_ThirdPositionToBeOpponent()
    {
        var am = AutoMock.GetStrict(x => x.RegisterType<PlayerManager>().As<IPlayerManager>());
        var manager = am.Create<TurnManager>();

        var players = WithPlayers();
        var startPlayer = players.Single(p => p.Id == 2);
        var round = new Round
        {
            StartPlayer = new()
            {
                new()
                {
                    Player = startPlayer,
                    PlayerId = startPlayer.Id
                }
            },
        };
        var result = manager.GetNextTurn(round, players);

        Assert.NotNull(result);
        Assert.Equal(2, result.Player.Position.Single().Position);
        Assert.Equal(3, result.OppositePlayer.Position.Single().Position);
    }

    [Fact]
    public void GetNextTurn_NoPreviousTurnsLastPositionIsStart_FirstPositionToBeOpponent()
    {
        var am = AutoMock.GetStrict(x => x.RegisterType<PlayerManager>().As<IPlayerManager>());
        var manager = am.Create<TurnManager>();

        var players = WithPlayers();
        var startPlayer = players.Single(p => p.Id == 4);
        var round = new Round
        {
            StartPlayer = new()
            {
                new()
                {
                    Player = startPlayer,
                    PlayerId = startPlayer.Id
                }
            },
        };
        var result = manager.GetNextTurn(round, players);

        Assert.NotNull(result);
        Assert.Equal(4, result.Player.Position.Single().Position);
        Assert.Equal(1, result.OppositePlayer.Position.Single().Position);
    }

    [Fact]
    public void GetNextTurn_HasPreviousTurnsSecondPositionTakes_ThirdPositionToAct()
    {
        var am = AutoMock.GetStrict(x => x.RegisterType<PlayerManager>().As<IPlayerManager>());
        var manager = am.Create<TurnManager>();

        var players = WithPlayers();
        var round = new Round
        {
            StartPlayer = new() { new() { Player = players.Single(p => p.Id == 2) } },
            Turns = WithTurns(1)
        };
        var result = manager.GetNextTurn(round, players);

        Assert.NotNull(result);
        Assert.Equal(3, result.Player.Position.Single().Position);
        Assert.Equal(4, result.OppositePlayer.Position.Single().Position);
    }

    [Fact]
    public void GetNextTurn_HasPreviousTurnsLastPositionToGo_FirstPositionToBeOpponent()
    {
        var am = AutoMock.GetStrict(x => x.RegisterType<PlayerManager>().As<IPlayerManager>());
        var manager = am.Create<TurnManager>();

        var players = WithPlayers();
        var round = new Round
        {
            StartPlayer = new() { new() { Player = players.Single(p => p.Id == 2) } },
            Turns = WithTurns(2)
        };
        var result = manager.GetNextTurn(round, players);

        Assert.NotNull(result);
        Assert.Equal(4, result.Player.Position.Single().Position);
        Assert.Equal(1, result.OppositePlayer.Position.Single().Position);
    }

    [Fact]
    public void GetNextTurn_HasPreviousTurnsLastPositionToGoFirstOut_SecondPositionToBeOpponent()
    {
        var am = AutoMock.GetStrict(x => x.RegisterType<PlayerManager>().As<IPlayerManager>());
        var manager = am.Create<TurnManager>();

        var players = WithPlayers(1, 3);
        var round = new Round
        {
            StartPlayer = new() { new() { Player = players.Single(p => p.Id == 2) } },
            Turns = WithTurns(2)

        };
        var result = manager.GetNextTurn(round, players);

        Assert.NotNull(result);
        Assert.Equal(4, result.Player.Position.Single().Position);
        Assert.Equal(2, result.OppositePlayer.Position.Single().Position);
    }

    [Fact]
    public void GetNextTurn_HasPreviousTurnsLastOut_FirstPositionToGo()
    {
        var am = AutoMock.GetStrict(x => x.RegisterType<PlayerManager>().As<IPlayerManager>());
        var manager = am.Create<TurnManager>();

        var players = WithPlayers(0, 3);
        var round = new Round
        {
            StartPlayer = new() { new() { Player = players.Single(p => p.Id == 1) } },
            Turns = WithTurns(2)
        };
        var result = manager.GetNextTurn(round, players);

        Assert.NotNull(result);
        Assert.Equal(1, result.Player.Position.Single().Position);
        Assert.Equal(2, result.OppositePlayer.Position.Single().Position);
    }

    private List<Turn> WithTurns(int take) =>
        new List<Turn>()
        {
            new()
            {
                PlayerId = 1,
                Player = new()
                {
                    Position = new()
                    {
                        new()
                        {
                            PlayerId = 1,
                            Position = 1
                        }
                    }
                },
                OppositePlayerId = 2,
                OppositePlayer = new()
                {
                    Position = new()
                    {
                        new()
                        {
                            PlayerId = 2,
                            Position = 2
                        }
                    }
                },
                Actions = new()
                {
                    new()
                    {
                        PlayerId = 1,
                        ActionType = ActionType.TOSS
                    },
                    new()
                    {
                        PlayerId = 2,
                        ActionType = ActionType.TAKE
                    },
                    new()
                    {
                        PlayerId = 1,
                        ActionType = ActionType.SKIP
                    },
                    new()
                    {
                        PlayerId = 3,
                        ActionType = ActionType.SKIP
                    },
                    new()
                    {
                        PlayerId = 4,
                        ActionType = ActionType.SKIP
                    }
                }
            },
            new()
            {
                PlayerId = 3,
                Player = new()
                {
                    Position = new()
                    {
                        new()
                        {
                            PlayerId = 3,
                            Position = 3
                        }
                    }
                },
                OppositePlayerId = 4,
                OppositePlayer = new()
                {
                    Position = new()
                    {
                        new()
                        {
                            PlayerId = 4,
                            Position = 4
                        }
                    }
                },
                Actions = new()
                {
                    new()
                    {
                        PlayerId = 3,
                        ActionType = ActionType.TOSS
                    },
                    new()
                    {
                        PlayerId = 4,
                        ActionType = ActionType.HIT
                    },
                    new()
                    {
                        PlayerId = 3,
                        ActionType = ActionType.SKIP
                    },
                    new()
                    {
                        PlayerId = 1,
                        ActionType = ActionType.SKIP
                    },
                    new()
                    {
                        PlayerId = 2,
                        ActionType = ActionType.SKIP
                    }
                }
            }
        }
        .Take(take)
        .ToList();

    private List<Player> WithPlayers(int skip = 0, int take = 4)
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
        }
        .Skip(skip)
        .Take(take)
        .ToList();
    }
}
