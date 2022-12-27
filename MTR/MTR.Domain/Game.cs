namespace MTR.Domain;

public record Game : IEntity
{
    public Guid Guid { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime? Started { get; set; }
    public DateTime? Ended { get; set; }
    public List<Round> Rounds { get; set; } = new();
    public List<Player> Players { get; set; } = new();
}
