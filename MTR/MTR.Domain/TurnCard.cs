﻿namespace MTR.Domain;

public record TurnCard : IBaseEntity
{
    public Action Action { get; set; }
    public RoundCard Card { get; set; }
    public TurnCard? OppositeCard { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
