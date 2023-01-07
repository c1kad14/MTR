namespace MTR.Domain;

public record Game : IEntity
{
    public Guid Guid { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public TableType TableType { get; set; }
    public List<GameStatus> Status { get; set; } = new();
    public List<Round> Rounds { get; set; } = new();
    public List<Player> Players { get; set; } = new();
}
