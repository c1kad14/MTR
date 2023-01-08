namespace MTR.Domain;

public record RoundReady : IEntity
{
    public Guid Guid { get; set; }
    public Player Player { get; set; }
    public int PlayerId { get; set; }
    public Round Round { get; set; }
    public int RoundId { get; set; }
}
