using Autofac.Extras.Moq;

using Moq;

using MTR.Core;
using MTR.Core.Abstractions;
using MTR.Domain;

namespace MTR.Tests;

public class ActionManagerTests
{
    [Fact]
    public void CanTossCards_FirstActionSingleCard_ReturnTrue()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        WithPlayerManager(am, 3);

        var players = WithPlayers();
        var round = new Round
        {
            Id = 1
        };

        var turn = new Turn
        {
            PlayerId = 1,
            Player = players.Single(p => p.Id == 1),
            OppositePlayerId = 2,
            OppositePlayer = players.Single(p => p.Id == 2),
        };

        var player = new Player
        {
            Id = 1,
        };

        var roundCards = new List<RoundCard>
        {
            new()
            {
                Id = 1,
                RoundId = round.Id,
                Round = round,
                Card = new()
                {
                    Id = 1,
                    Rank = Rank.SIX
                }
            }
        };

        var result = manager.CanToss(round, turn, player, roundCards);

        Assert.True(result);
    }

    [Fact]
    public void CanTossCards_FirstActionTwoSimilarCards_ReturnTrue()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        WithPlayerManager(am, 3);

        var players = WithPlayers();
        var round = new Round
        {
            Id = 1
        };

        var turn = new Turn
        {
            PlayerId = 1,
            Player = players.Single(p => p.Id == 1),
            OppositePlayerId = 2,
            OppositePlayer = players.Single(p => p.Id == 2),
        };

        var player = new Player
        {
            Id = 1,
        };

        var roundCards = new List<RoundCard>
        {
            new()
            {
                Id = 1,
                RoundId = round.Id,
                Round = round,
                Card = new()
                {
                    Id = 1,
                    Rank = Rank.SIX,
                }
            },
            new()
            {
                Id = 2,
                RoundId = round.Id,
                Round = round,
                Card = new()
                {
                    Id = 2,
                    Rank = Rank.SIX,
                }
            },
        };

        var result = manager.CanToss(round, turn, player, roundCards);

        Assert.True(result);
    }

    [Fact]
    public void CanTossCards_FirstActionTwoDifferentCards_ReturnFalse()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        WithPlayerManager(am, 3);

        var players = WithPlayers();
        var round = new Round
        {
            Id = 1
        };

        var turn = new Turn
        {
            PlayerId = 1,
            Player = players.Single(p => p.Id == 1),
            OppositePlayerId = 2,
            OppositePlayer = players.Single(p => p.Id == 2),
        };

        var player = new Player
        {
            Id = 1,
        };

        var roundCards = new List<RoundCard>
        {
            new()
            {
                Id = 1,
                RoundId = round.Id,
                Round = round,
                Card = new()
                {
                    Id = 1,
                    Rank = Rank.SIX,
                }
            },
            new()
            {
                Id = 2,
                RoundId = round.Id,
                Round = round,
                Card = new()
                {
                    Id = 2,
                    Rank = Rank.SEVEN,
                }
            },
        };

        var result = manager.CanToss(round, turn, player, roundCards);

        Assert.False(result);
    }

    [Fact]
    public void CanTossCards_SecondActionSingleCardNotOnBoard_ReturnFalse()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        WithPlayerManager(am, 3);

        var players = WithPlayers();
        var round = new Round
        {
            Id = 1
        };

        var turn = new Turn
        {
            PlayerId = players.Single(p => p.Id == 1).Id,
            Player = players.Single(p => p.Id == 1),
            OppositePlayerId = players.Single(p => p.Id == 2).Id,
            OppositePlayer = players.Single(p => p.Id == 2),
            Actions = new()
            {
                new()
                {
                    PlayerId = players.Single(p => p.Id == 1).Id,
                    TurnCards = new()
                    {
                        new()
                        {
                            RoundCard = new()
                            {
                                Id = 1,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 1,
                                    Rank = Rank.SEVEN
                                }
                            }
                        }
                    }
                }
            },
            TurnCards = new()
            {
                new()
                {
                    RoundCard = new()
                    {
                        Id = 1,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 1,
                            Rank = Rank.SEVEN
                        }
                    }
                }
            }
        };

        var roundCards = new List<RoundCard>
        {
            new()
            {
                Id = 2,
                RoundId = round.Id,
                Round = round,
                Card = new()
                {
                    Id = 2,
                    Rank = Rank.SIX
                }
            }
        };

        var result = manager.CanToss(round, turn, players.Single(p => p.Id == 1), roundCards);

        Assert.False(result);
    }

    [Fact]
    public void CanTossCards_SecondActionSingleCardOnBoard_ReturnTrue()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        WithPlayerManager(am, 3);

        var players = WithPlayers();
        var round = new Round
        {
            Id = 1
        };

        var turn = new Turn
        {
            PlayerId = players.Single(p => p.Id == 1).Id,
            Player = players.Single(p => p.Id == 1),
            OppositePlayerId = players.Single(p => p.Id == 2).Id,
            OppositePlayer = players.Single(p => p.Id == 2),
            Actions = new()
            {
                new()
                {
                    PlayerId = players.Single(p => p.Id == 1).Id,
                    TurnCards = new()
                    {
                        new()
                        {
                            RoundCard = new()
                            {
                                Id = 1,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 1,
                                    Rank = Rank.SIX
                                }
                            }
                        }
                    }
                }
            },
            TurnCards = new()
            {
                new()
                {
                    RoundCard = new()
                    {
                        Id = 1,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 1,
                            Rank = Rank.SIX
                        }
                    }
                }
            }
        };

        var roundCards = new List<RoundCard>
        {
            new()
            {
                Id = 2,
                RoundId = round.Id,
                Round = round,
                Card = new()
                {
                    Id = 2,
                    Rank = Rank.SIX
                }
            }
        };

        var result = manager.CanToss(round, turn, players.Single(p => p.Id == 1), roundCards);

        Assert.True(result);
    }

    [Fact]
    public void CanTossCards_ThirdActionSingleCardOppositeCardOnBoard_ReturnTrue()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        WithPlayerManager(am, 3);

        var players = WithPlayers();
        var round = new Round
        {
            Id = 1
        };

        var turn = new Turn
        {
            PlayerId = players.Single(p => p.Id == 1).Id,
            Player = players.Single(p => p.Id == 1),
            OppositePlayerId = players.Single(p => p.Id == 2).Id,
            OppositePlayer = players.Single(p => p.Id == 2),
            Actions = new()
            {
                new()
                {
                    PlayerId = players.Single(p => p.Id == 1).Id,
                    TurnCards = new()
                    {
                        new()
                        {
                            RoundCard = new()
                            {
                                Id = 1,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 1,
                                    Rank = Rank.SIX
                                }
                            }
                        }
                    }
                },
                new()
                {
                    PlayerId = players.Single(p => p.Id == 2).Id,
                    TurnCards = new()
                    {
                        new()
                        {
                            RoundCard = new()
                            {
                                Id = 2,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 2,
                                    Rank = Rank.SEVEN
                                }
                            }
                        }
                    }
                },
            },
            TurnCards = new()
            {
                new()
                {
                    RoundCard = new()
                    {
                        Id = 1,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 1,
                            Rank = Rank.SIX
                        }
                    },
                },
                new()
                {
                    OppositeTurnCardId = 1,
                    OppositeTurnCard = new()
                    {
                        RoundCardId = 1,
                        RoundCard = new()
                        {
                            Id = 1,
                            RoundId = round.Id,
                            Round = round,
                            Card = new()
                            {
                                Id = 1,
                                Rank = Rank.SIX
                            }
                        },
                    },
                    RoundCard = new()
                    {
                        Id = 2,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 2,
                            Rank = Rank.SEVEN
                        }
                    },
                },
            }
        };

        var roundCards = new List<RoundCard>
        {
            new()
            {
                Id = 3,
                RoundId = round.Id,
                Round = round,
                Card = new()
                {
                    Id = 3,
                    Rank = Rank.SEVEN
                }
            }
        };

        var result = manager.CanToss(round, turn, players.Single(p => p.Id == 1), roundCards);

        Assert.True(result);
    }

    [Fact]
    public void CanTossCards_ThirdActionSingleCardOppositeCardNotOnBoard_ReturnFalse()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        WithPlayerManager(am, 3);

        var players = WithPlayers();
        var round = new Round
        {
            Id = 1
        };

        var turn = new Turn
        {
            PlayerId = players.Single(p => p.Id == 1).Id,
            Player = players.Single(p => p.Id == 1),
            OppositePlayerId = players.Single(p => p.Id == 2).Id,
            OppositePlayer = players.Single(p => p.Id == 2),
            Actions = new()
            {
                new()
                {
                    PlayerId = players.Single(p => p.Id == 1).Id,
                    TurnCards = new()
                    {
                        new()
                        {
                            RoundCard = new()
                            {
                                Id = 1,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 1,
                                    Rank = Rank.SIX
                                }
                            }
                        }
                    }
                },
                new()
                {
                    PlayerId = players.Single(p => p.Id == 2).Id,
                    TurnCards = new()
                    {
                        new()
                        {
                            RoundCard = new()
                            {
                                Id = 2,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 2,
                                    Rank = Rank.SEVEN
                                }
                            }
                        }
                    }
                },
            },
            TurnCards = new()
            {
                new()
                {
                    RoundCard = new()
                    {
                        Id = 1,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 1,
                            Rank = Rank.SIX
                        }
                    },
                },
                new()
                {
                    OppositeTurnCardId = 1,
                    OppositeTurnCard = new()
                    {
                        RoundCardId = 1,
                        RoundCard = new()
                        {
                            Id = 1,
                            RoundId = round.Id,
                            Round = round,
                            Card = new()
                            {
                                Id = 1,
                                Rank = Rank.SIX
                            }
                        },
                    },
                    RoundCard = new()
                    {
                        Id = 2,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 2,
                            Rank = Rank.SEVEN
                        }
                    },
                },
            }
        };

        var roundCards = new List<RoundCard>
        {
            new()
            {
                Id = 3,
                RoundId = round.Id,
                Round = round,
                Card = new()
                {
                    Id = 3,
                    Rank = Rank.EIGHT
                }
            }
        };

        var result = manager.CanToss(round, turn, players.Single(p => p.Id == 1), roundCards);

        Assert.False(result);
    }

    [Fact]
    public void CanTossCards_SecondActionSingleCardOnBoardNextPlayer_ReturnTrue()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        WithPlayerManager(am, 3);

        var players = WithPlayers();
        var round = new Round
        {
            Id = 1
        };

        var turn = new Turn
        {
            PlayerId = players.Single(p => p.Id == 1).Id,
            Player = players.Single(p => p.Id == 1),
            OppositePlayerId = players.Single(p => p.Id == 2).Id,
            OppositePlayer = players.Single(p => p.Id == 2),
            Actions = new()
            {
                new()
                {
                    PlayerId = players.Single(p => p.Id == 1).Id,
                    TurnCards = new()
                    {
                        new()
                        {
                            RoundCard = new()
                            {
                                Id = 1,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 1,
                                    Rank = Rank.SEVEN
                                }
                            }
                        }
                    }
                }
            },
            TurnCards = new()
            {
                new()
                {
                    RoundCard = new()
                    {
                        Id = 1,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 1,
                            Rank = Rank.SEVEN
                        }
                    }
                }
            }
        };

        var roundCards = new List<RoundCard>
        {
            new()
            {
                Id = 2,
                RoundId = round.Id,
                Round = round,
                Card = new()
                {
                    Id = 2,
                    Rank = Rank.SEVEN
                }
            }
        };

        var result = manager.CanToss(round, turn, players.Single(p => p.Id == 3), roundCards);

        Assert.True(result);
    }

    [Fact]
    public void CanTossCards_SecondActionSingleCardOnBoardOtherPlayer_ReturnTrue()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        WithPlayerManager(am, 3);

        var players = WithPlayers();
        var round = new Round
        {
            Id = 1
        };

        var turn = new Turn
        {
            PlayerId = players.Single(p => p.Id == 1).Id,
            Player = players.Single(p => p.Id == 1),
            OppositePlayerId = players.Single(p => p.Id == 2).Id,
            OppositePlayer = players.Single(p => p.Id == 2),
            Actions = new()
            {
                new()
                {
                    PlayerId = players.Single(p => p.Id == 1).Id,
                    TurnCards = new()
                    {
                        new()
                        {
                            RoundCard = new()
                            {
                                Id = 1,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 1,
                                    Rank = Rank.SEVEN
                                }
                            }
                        }
                    }
                }
            },
            TurnCards = new()
            {
                new()
                {
                    RoundCard = new()
                    {
                        Id = 1,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 1,
                            Rank = Rank.SEVEN
                        }
                    }
                }
            }
        };

        var roundCards = new List<RoundCard>
        {
            new()
            {
                Id = 2,
                RoundId = round.Id,
                Round = round,
                Card = new()
                {
                    Id = 2,
                    Rank = Rank.SEVEN
                }
            }
        };

        var result = manager.CanToss(round, turn, players.Single(p => p.Id == 4), roundCards);

        Assert.True(result);
    }

    [Fact]
    public void CanTossCards_ThirdActionTwoCardsOnBoardNextPlayerOppositeCard_ReturnTrue()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        WithPlayerManager(am, 3);

        var players = WithPlayers();
        var round = new Round
        {
            Id = 1
        };

        var turn = new Turn
        {
            PlayerId = players.Single(p => p.Id == 1).Id,
            Player = players.Single(p => p.Id == 1),
            OppositePlayerId = players.Single(p => p.Id == 2).Id,
            OppositePlayer = players.Single(p => p.Id == 2),
            Actions = new()
            {
                new()
                {
                    PlayerId = players.Single(p => p.Id == 1).Id,
                    TurnCards = new()
                    {
                        new()
                        {
                            Id = 1,
                            RoundCard = new()
                            {
                                Id = 1,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 1,
                                    Rank = Rank.SEVEN
                                }
                            }
                        },
                        new()
                        {
                            OppositeTurnCardId = 1,
                            OppositeTurnCard = new()
                            {
                                Id = 1,
                            },
                            RoundCard = new()
                            {
                                Id = 2,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 2,
                                    Rank = Rank.ACE
                                }
                            }
                        },
                    }
                }
            },
            TurnCards = new()
            {
                new()
                {
                    RoundCard = new()
                    {
                        Id = 1,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 1,
                            Rank = Rank.SEVEN
                        }
                    }
                },
                new()
                {
                    OppositeTurnCardId = 1,
                    OppositeTurnCard = new()
                    {
                        Id = 1,
                    },
                    RoundCard = new()
                    {
                        Id = 2,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 2,
                            Rank = Rank.ACE
                        }
                    }
                },
            }
        };

        var roundCards = new List<RoundCard>
        {
            new()
            {
                Id = 2,
                RoundId = round.Id,
                Round = round,
                Card = new()
                {
                    Id = 2,
                    Rank = Rank.ACE
                }
            }
        };

        var result = manager.CanToss(round, turn, players.Single(p => p.Id == 3), roundCards);

        Assert.True(result);
    }

    [Fact]
    public void CanTossCards_ThirdActionTwoCardsOnBoardOtherPlayerOppositeCard_ReturnFalse()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        WithPlayerManager(am, 3);

        var players = WithPlayers();
        var round = new Round
        {
            Id = 1
        };

        var turn = new Turn
        {
            PlayerId = players.Single(p => p.Id == 1).Id,
            Player = players.Single(p => p.Id == 1),
            OppositePlayerId = players.Single(p => p.Id == 2).Id,
            OppositePlayer = players.Single(p => p.Id == 2),
            Actions = new()
            {
                new()
                {
                    PlayerId = players.Single(p => p.Id == 1).Id,
                    TurnCards = new()
                    {
                        new()
                        {
                            Id = 1,
                            RoundCard = new()
                            {
                                Id = 1,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 1,
                                    Rank = Rank.SEVEN
                                }
                            }
                        },
                        new()
                        {
                            OppositeTurnCardId = 1,
                            OppositeTurnCard = new()
                            {
                                Id = 1,
                            },
                            RoundCard = new()
                            {
                                Id = 2,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 2,
                                    Rank = Rank.ACE
                                }
                            }
                        },
                    }
                }
            },
            TurnCards = new()
            {
                new()
                {
                    RoundCard = new()
                    {
                        Id = 1,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 1,
                            Rank = Rank.SEVEN
                        }
                    }
                },
                new()
                {
                    OppositeTurnCardId = 1,
                    OppositeTurnCard = new()
                    {
                        Id = 1,
                    },
                    RoundCard = new()
                    {
                        Id = 2,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 2,
                            Rank = Rank.ACE
                        }
                    }
                },
            }
        };

        var roundCards = new List<RoundCard>
        {
            new()
            {
                Id = 2,
                RoundId = round.Id,
                Round = round,
                Card = new()
                {
                    Id = 2,
                    Rank = Rank.ACE
                }
            }
        };

        var result = manager.CanToss(round, turn, players.Single(p => p.Id == 4), roundCards);

        Assert.False(result);
    }

    [Fact]
    public void CanTossCards_SixthCardToHitNoMuck_ReturnFalse()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        WithPlayerManager(am, 3);

        var players = WithPlayers();
        var round = new Round
        {
            Id = 1
        };

        var turn = new Turn
        {
            PlayerId = players.Single(p => p.Id == 1).Id,
            Player = players.Single(p => p.Id == 1),
            OppositePlayerId = players.Single(p => p.Id == 2).Id,
            OppositePlayer = players.Single(p => p.Id == 2),
            Actions = new()
            {
                new()
                {
                    PlayerId = players.Single(p => p.Id == 1).Id,
                    TurnCards = new()
                    {
                        new()
                        {
                            Id = 1,
                            RoundCard = new()
                            {
                                Id = 1,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 1,
                                    Rank = Rank.SEVEN
                                }
                            }
                        },
                        new()
                        {
                            OppositeTurnCardId = 1,
                            OppositeTurnCard = new()
                            {
                                Id = 1,
                            },
                            RoundCard = new()
                            {
                                Id = 2,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 2,
                                    Rank = Rank.ACE
                                }
                            }
                        },
                    }
                }
            },
            TurnCards = new()
            {
                new()
                {
                    Id = 1,
                    RoundCard = new()
                    {
                        Id = 1,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 1,
                            Rank = Rank.SEVEN
                        }
                    }
                },
                new()
                {
                    Id = 2,
                    OppositeTurnCardId = 1,
                    OppositeTurnCard = new()
                    {
                        Id = 1,
                    },
                    RoundCard = new()
                    {
                        Id = 2,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 2,
                            Rank = Rank.ACE
                        }
                    }
                },
                new()
                {
                    Id = 3,
                    RoundCard = new()
                    {
                        Id = 3,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 3,
                            Rank = Rank.SEVEN
                        }
                    }
                },
                new()
                {
                    Id = 4,
                    OppositeTurnCardId = 3,
                    OppositeTurnCard = new()
                    {
                        Id = 3,
                    },
                    RoundCard = new()
                    {
                        Id = 4,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 4,
                            Rank = Rank.ACE
                        }
                    }
                },
                new()
                {
                    Id = 5,
                    RoundCard = new()
                    {
                        Id = 5,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 5,
                            Rank = Rank.SEVEN
                        }
                    }
                },
                new()
                {
                    Id = 6,
                    OppositeTurnCardId = 5,
                    OppositeTurnCard = new()
                    {
                        Id = 5,
                    },
                    RoundCard = new()
                    {
                        Id = 6,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 6,
                            Rank = Rank.ACE
                        }
                    }
                },
                new()
                {
                    Id = 7,
                    RoundCard = new()
                    {
                        Id = 7,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 7,
                            Rank = Rank.SEVEN
                        }
                    }
                },
                new()
                {
                    OppositeTurnCardId = 7,
                    OppositeTurnCard = new()
                    {
                        Id = 8,
                    },
                    RoundCard = new()
                    {
                        Id = 8,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 8,
                            Rank = Rank.KING
                        }
                    }
                },
                new()
                {
                    Id = 9,
                    RoundCard = new()
                    {
                        Id = 9,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 0,
                            Rank = Rank.ACE
                        }
                    }
                }
            }
        };

        var roundCards = new List<RoundCard>
        {
            new()
            {
                Id = 10,
                RoundId = round.Id,
                Round = round,
                Card = new()
                {
                    Id = 10,
                    Rank = Rank.KING
                }
            }
        };

        var result = manager.CanToss(round, turn, players.Single(p => p.Id == 3), roundCards);

        Assert.False(result);
    }

    [Fact]
    public void CanTossCards_SixthCardToHitHasMuck_ReturnTrue()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        WithPlayerManager(am, 3);

        var players = WithPlayers();
        var round = new Round
        {
            Id = 1,
            RoundCards = new()
            {
                new()
                {
                    Id = 22,
                    MuckedCards = new()
                    {
                        new()
                        {
                            RoundCardId = 22
                        }
                    }
                }
            }
        };

        var turn = new Turn
        {
            PlayerId = players.Single(p => p.Id == 1).Id,
            Player = players.Single(p => p.Id == 1),
            OppositePlayerId = players.Single(p => p.Id == 2).Id,
            OppositePlayer = players.Single(p => p.Id == 2),
            Actions = new()
            {
                new()
                {
                    PlayerId = players.Single(p => p.Id == 1).Id,
                    TurnCards = new()
                    {
                        new()
                        {
                            Id = 1,
                            RoundCard = new()
                            {
                                Id = 1,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 1,
                                    Rank = Rank.SEVEN
                                }
                            }
                        },
                        new()
                        {
                            OppositeTurnCardId = 1,
                            OppositeTurnCard = new()
                            {
                                Id = 1,
                            },
                            RoundCard = new()
                            {
                                Id = 2,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 2,
                                    Rank = Rank.ACE
                                }
                            }
                        },
                    }
                }
            },
            TurnCards = new()
            {
                new()
                {
                    Id = 1,
                    RoundCard = new()
                    {
                        Id = 1,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 1,
                            Rank = Rank.SEVEN
                        }
                    }
                },
                new()
                {
                    Id = 2,
                    OppositeTurnCardId = 1,
                    OppositeTurnCard = new()
                    {
                        Id = 1,
                    },
                    RoundCard = new()
                    {
                        Id = 2,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 2,
                            Rank = Rank.ACE
                        }
                    }
                },
                new()
                {
                    Id = 3,
                    RoundCard = new()
                    {
                        Id = 3,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 3,
                            Rank = Rank.SEVEN
                        }
                    }
                },
                new()
                {
                    Id = 4,
                    OppositeTurnCardId = 3,
                    OppositeTurnCard = new()
                    {
                        Id = 3,
                    },
                    RoundCard = new()
                    {
                        Id = 4,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 4,
                            Rank = Rank.ACE
                        }
                    }
                },
                new()
                {
                    Id = 5,
                    RoundCard = new()
                    {
                        Id = 5,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 5,
                            Rank = Rank.SEVEN
                        }
                    }
                },
                new()
                {
                    Id = 6,
                    OppositeTurnCardId = 5,
                    OppositeTurnCard = new()
                    {
                        Id = 5,
                    },
                    RoundCard = new()
                    {
                        Id = 6,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 6,
                            Rank = Rank.ACE
                        }
                    }
                },
                new()
                {
                    Id = 7,
                    RoundCard = new()
                    {
                        Id = 7,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 7,
                            Rank = Rank.SEVEN
                        }
                    }
                },
                new()
                {
                    OppositeTurnCardId = 7,
                    OppositeTurnCard = new()
                    {
                        Id = 8,
                    },
                    RoundCard = new()
                    {
                        Id = 8,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 8,
                            Rank = Rank.KING
                        }
                    }
                },
                new()
                {
                    Id = 9,
                    RoundCard = new()
                    {
                        Id = 9,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 0,
                            Rank = Rank.ACE
                        }
                    }
                }
            }
        };

        var roundCards = new List<RoundCard>
        {
            new()
            {
                Id = 10,
                RoundId = round.Id,
                Round = round,
                Card = new()
                {
                    Id = 10,
                    Rank = Rank.KING
                }
            }
        };

        var result = manager.CanToss(round, turn, players.Single(p => p.Id == 3), roundCards);

        Assert.True(result);
    }

    [Fact]
    public void CanTossCards_SeventCardToHitHasMuck_ReturnFalse()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        WithPlayerManager(am, 3);

        var players = WithPlayers();
        var round = new Round
        {
            Id = 1,
            RoundCards = new()
            {
                new()
                {
                    Id = 22,
                    MuckedCards = new()
                    {
                        new()
                        {
                            RoundCardId = 22
                        }
                    }
                }
            }
        };

        var turn = new Turn
        {
            PlayerId = players.Single(p => p.Id == 1).Id,
            Player = players.Single(p => p.Id == 1),
            OppositePlayerId = players.Single(p => p.Id == 2).Id,
            OppositePlayer = players.Single(p => p.Id == 2),
            Actions = new()
            {
                new()
                {
                    PlayerId = players.Single(p => p.Id == 1).Id,
                    TurnCards = new()
                    {
                        new()
                        {
                            Id = 1,
                            RoundCard = new()
                            {
                                Id = 1,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 1,
                                    Rank = Rank.SEVEN
                                }
                            }
                        },
                        new()
                        {
                            OppositeTurnCardId = 1,
                            OppositeTurnCard = new()
                            {
                                Id = 1,
                            },
                            RoundCard = new()
                            {
                                Id = 2,
                                RoundId = round.Id,
                                Round = round,
                                Card = new()
                                {
                                    Id = 2,
                                    Rank = Rank.ACE
                                }
                            }
                        },
                    }
                }
            },
            TurnCards = new()
            {
                new()
                {
                    Id = 1,
                    RoundCard = new()
                    {
                        Id = 1,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 1,
                            Rank = Rank.SEVEN
                        }
                    }
                },
                new()
                {
                    Id = 2,
                    OppositeTurnCardId = 1,
                    OppositeTurnCard = new()
                    {
                        Id = 1,
                    },
                    RoundCard = new()
                    {
                        Id = 2,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 2,
                            Rank = Rank.ACE
                        }
                    }
                },
                new()
                {
                    Id = 3,
                    RoundCard = new()
                    {
                        Id = 3,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 3,
                            Rank = Rank.SEVEN
                        }
                    }
                },
                new()
                {
                    Id = 4,
                    OppositeTurnCardId = 3,
                    OppositeTurnCard = new()
                    {
                        Id = 3,
                    },
                    RoundCard = new()
                    {
                        Id = 4,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 4,
                            Rank = Rank.ACE
                        }
                    }
                },
                new()
                {
                    Id = 5,
                    RoundCard = new()
                    {
                        Id = 5,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 5,
                            Rank = Rank.SEVEN
                        }
                    }
                },
                new()
                {
                    Id = 6,
                    OppositeTurnCardId = 5,
                    OppositeTurnCard = new()
                    {
                        Id = 5,
                    },
                    RoundCard = new()
                    {
                        Id = 6,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 6,
                            Rank = Rank.ACE
                        }
                    }
                },
                new()
                {
                    Id = 7,
                    RoundCard = new()
                    {
                        Id = 7,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 7,
                            Rank = Rank.SEVEN
                        }
                    }
                },
                new()
                {
                    OppositeTurnCardId = 7,
                    OppositeTurnCard = new()
                    {
                        Id = 8,
                    },
                    RoundCard = new()
                    {
                        Id = 8,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 8,
                            Rank = Rank.KING
                        }
                    }
                },
                new()
                {
                    Id = 9,
                    RoundCard = new()
                    {
                        Id = 9,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 9,
                            Rank = Rank.ACE
                        }
                    }
                },
                new()
                {
                    Id = 11,
                    RoundCard = new()
                    {
                        Id = 11,
                        RoundId = round.Id,
                        Round = round,
                        Card = new()
                        {
                            Id = 11,
                            Rank = Rank.ACE
                        }
                    }
                }
            }
        };

        var roundCards = new List<RoundCard>
        {
            new()
            {
                Id = 10,
                RoundId = round.Id,
                Round = round,
                Card = new()
                {
                    Id = 10,
                    Rank = Rank.KING
                }
            }
        };

        var result = manager.CanToss(round, turn, players.Single(p => p.Id == 3), roundCards);

        Assert.False(result);
    }

    [Fact]
    public void CanHit_SevenOverSixNoSuit_ReturnTrue()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        var round = new Round
        {
            Suit = Suit.SPADES
        };

        var cardToHit = new TurnCard
        {
            RoundCard = new()
            {
                Card = new()
                {
                    Suit = Suit.HEARTS,
                    Rank = Rank.SIX
                }
            }
        };

        var oppositeCard = new RoundCard
        {
            Card = new()
            {
                Suit = Suit.HEARTS,
                Rank = Rank.SEVEN
            }
        };

        var result = manager.CanHit(round, cardToHit, oppositeCard);

        Assert.True(result);
    }

    [Fact]
    public void CanHit_SevenOverSevenNoSuit_ReturnFalse()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        var round = new Round
        {
            Suit = Suit.SPADES
        };

        var cardToHit = new TurnCard
        {
            RoundCard = new()
            {
                Card = new()
                {
                    Suit = Suit.HEARTS,
                    Rank = Rank.SEVEN
                }
            }
        };

        var oppositeCard = new RoundCard
        {
            Card = new()
            {
                Suit = Suit.HEARTS,
                Rank = Rank.SEVEN
            }
        };

        var result = manager.CanHit(round, cardToHit, oppositeCard);

        Assert.False(result);
    }

    [Fact]
    public void CanHit_AceOverTenNoSuit_ReturnTrue()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        var round = new Round
        {
            Suit = Suit.SPADES
        };

        var cardToHit = new TurnCard
        {
            RoundCard = new()
            {
                Card = new()
                {
                    Suit = Suit.HEARTS,
                    Rank = Rank.TEN
                }
            }
        };

        var oppositeCard = new RoundCard
        {
            Card = new()
            {
                Suit = Suit.HEARTS,
                Rank = Rank.ACE
            }
        };

        var result = manager.CanHit(round, cardToHit, oppositeCard);

        Assert.True(result);
    }

    [Fact]
    public void CanHit_SixOverSevenOneSuit_ReturnTrue()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        var round = new Round
        {
            Suit = Suit.SPADES
        };

        var cardToHit = new TurnCard
        {
            RoundCard = new()
            {
                Card = new()
                {
                    Suit = Suit.HEARTS,
                    Rank = Rank.SEVEN
                }
            }
        };

        var oppositeCard = new RoundCard
        {
            Card = new()
            {
                Suit = Suit.SPADES,
                Rank = Rank.SIX
            }
        };

        var result = manager.CanHit(round, cardToHit, oppositeCard);

        Assert.True(result);
    }

    [Fact]
    public void CanHit_SixOverSevenBothSuit_ReturnFalse()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        var round = new Round
        {
            Suit = Suit.SPADES
        };

        var cardToHit = new TurnCard
        {
            RoundCard = new()
            {
                Card = new()
                {
                    Suit = Suit.SPADES,
                    Rank = Rank.SEVEN
                }
            }
        };

        var oppositeCard = new RoundCard
        {
            Card = new()
            {
                Suit = Suit.SPADES,
                Rank = Rank.SIX
            }
        };

        var result = manager.CanHit(round, cardToHit, oppositeCard);

        Assert.False(result);
    }

    [Fact]
    public void CanHit_EightOverSevenBothSuit_ReturnTrue()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        var round = new Round
        {
            Suit = Suit.SPADES
        };

        var cardToHit = new TurnCard
        {
            RoundCard = new()
            {
                Card = new()
                {
                    Suit = Suit.SPADES,
                    Rank = Rank.SEVEN
                }
            }
        };

        var oppositeCard = new RoundCard
        {
            Card = new()
            {
                Suit = Suit.SPADES,
                Rank = Rank.EIGHT
            }
        };

        var result = manager.CanHit(round, cardToHit, oppositeCard);

        Assert.True(result);
    }

    [Fact]
    public void CanHit_KingOverQueenBothSuit_ReturnTrue()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        var round = new Round
        {
            Suit = Suit.SPADES
        };

        var cardToHit = new TurnCard
        {
            RoundCard = new()
            {
                Card = new()
                {
                    Suit = Suit.SPADES,
                    Rank = Rank.QUEEN
                }
            }
        };

        var oppositeCard = new RoundCard
        {
            Card = new()
            {
                Suit = Suit.SPADES,
                Rank = Rank.KING
            }
        };

        var result = manager.CanHit(round, cardToHit, oppositeCard);

        Assert.True(result);
    }

    [Fact]
    public void CanHit_SixOverAceOneSuit_ReturnTrue()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        var round = new Round
        {
            Suit = Suit.SPADES
        };

        var cardToHit = new TurnCard
        {
            RoundCard = new()
            {
                Card = new()
                {
                    Suit = Suit.CLUBS,
                    Rank = Rank.ACE
                }
            }
        };

        var oppositeCard = new RoundCard
        {
            Card = new()
            {
                Suit = Suit.SPADES,
                Rank = Rank.SIX
            }
        };

        var result = manager.CanHit(round, cardToHit, oppositeCard);

        Assert.True(result);
    }

    [Fact]
    public void CanHit_AceOverSixOneSuit_ReturnFalse()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        var round = new Round
        {
            Suit = Suit.SPADES
        };

        var cardToHit = new TurnCard
        {
            RoundCard = new()
            {
                Card = new()
                {
                    Suit = Suit.SPADES,
                    Rank = Rank.SIX
                }
            }
        };

        var oppositeCard = new RoundCard
        {
            Card = new()
            {
                Suit = Suit.CLUBS,
                Rank = Rank.ACE
            }
        };

        var result = manager.CanHit(round, cardToHit, oppositeCard);

        Assert.False(result);
    }

    [Fact]
    public void CanHit_AceOverSixDifferentSuitsNoSuit_ReturnFalse()
    {
        var am = AutoMock.GetLoose();
        var manager = am.Create<ActionManager>();

        var round = new Round
        {
            Suit = Suit.SPADES
        };

        var cardToHit = new TurnCard
        {
            RoundCard = new()
            {
                Card = new()
                {
                    Suit = Suit.HEARTS,
                    Rank = Rank.SIX
                }
            }
        };

        var oppositeCard = new RoundCard
        {
            Card = new()
            {
                Suit = Suit.CLUBS,
                Rank = Rank.ACE
            }
        };

        var result = manager.CanHit(round, cardToHit, oppositeCard);

        Assert.False(result);
    }

    private void WithPlayerManager(AutoMock am, int nextPlayerId)
    {
        var players = WithPlayers();
        am.Mock<IPlayerManager>().Setup(pm => pm.GetPlayersWithCards(It.IsAny<Round>())).Returns(players);
        am.Mock<IPlayerManager>().Setup(pm => pm.GetNextPlayer(It.IsAny<List<Player>>(), It.IsAny<int>())).Returns(players.Single(p => p.Id == nextPlayerId));
    }

    private List<Player> WithPlayers() =>
        new()
        {
            new()
            {
                Id = 1,
                Position = new()
                {
                    new()
                    {
                        Id = 1,
                        Position = 1
                    }
                }
            },
            new()
            {
                Id = 2,
                Position = new()
                {
                    new()
                    {
                        Id = 2,
                        Position = 2
                    }
                }
            },
            new()
            {
                Id = 3,
                Position = new()
                {
                    new()
                    {
                        Id = 3,
                        Position = 3
                    }
                }
            },
            new()
            {
                Id = 4,
                Position = new()
                {
                    new()
                    {
                        Id = 4,
                        Position = 4
                    }
                }
            },
        };
}
