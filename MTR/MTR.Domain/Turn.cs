namespace MTR.Domain;

public record Turn : IEntity
{
    public Player Player { get; set; }
    public int PlayerId { get; set; }
    public Player OppositePlayer { get; set; }
    public int OppositePlayerId { get; set; }
    public Round Round { get; set; }
    public int RoundId { get; set; }
    public List<Action> Actions { get; set; }
    public List<TurnCard> TurnCards { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
