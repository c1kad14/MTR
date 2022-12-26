namespace MTR.Domain;

public record Game : IBaseEntity
{
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime? Started { get; set; }
    public DateTime? Ended { get; set; }
    public List<Round> Rounds { get; set; } = new();
    public List<Player> Players { get; set; } = new();
}
