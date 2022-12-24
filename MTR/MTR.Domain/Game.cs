namespace MTR.Domain;

public record Game : IBaseEntity
{
    public DateTime Started { get; set; }
    public DateTime? Ended { get; set; }
    public List<Round> Rounds { get; set; }
    public List<Player> Players { get; set; }
}
