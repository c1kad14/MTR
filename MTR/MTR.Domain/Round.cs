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
    public DateTime? Ended { get; set; }
    public List<RoundCard> Cards { get; set; }
    public List<RoundResult> RoundResults { get; set; }
}
