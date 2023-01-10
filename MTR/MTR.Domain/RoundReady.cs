namespace MTR.Domain;

public record RoundReady : IEntity
{
    public Player Player { get; set; }
    public int PlayerId { get; set; }
    public Round Round { get; set; }
    public int RoundId { get; set; }
    public bool Ready { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
