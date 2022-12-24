namespace MTR.Domain;

public record Round : IBaseEntity
{
    public int Sequence { get; set; }
    public Game Game { get; set; }
    public Suit Suit { get; set; }
    public DateTime Started { get; set; }
    public DateTime? Ended { get; set; }
    public List<RoundResult> RoundResults { get; set; }
}
