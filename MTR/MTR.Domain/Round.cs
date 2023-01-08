namespace MTR.Domain;

public record Round : IEntity
{
    public Guid Guid { get; set; }
    public int Sequence { get; set; }
    public Game Game { get; set; }
    public int GameId { get; set; }
    public Suit Suit { get; set; }
    public int StartPosition { get; set; }
    public DateTime Started { get; set; } = DateTime.UtcNow;
    public List<RoundStatus> Status { get; set; } = new();
    public List<Turn> Turns { get; set; } = new();
    public List<RoundCard> RoundCards { get; set; } = new();
    public List<RoundReady> RoundReady { get; set; } = new();
    public List<RoundResult> RoundResults { get; set; } = new();
}
