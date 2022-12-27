namespace MTR.Domain;

public record RoundResult : IEntity
{
    public Round Round { get; set; }
    public int RoundId { get; set; }
    public Player Player { get; set; }
    public int PlayerId { get; set; }
    public int Score { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
