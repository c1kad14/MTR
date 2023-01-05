namespace MTR.Domain;

public record RoundStatus : IEntity
{
    public Round Round { get; set; }
    public int RoundId { get; set; }
    public StatusType Status { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
