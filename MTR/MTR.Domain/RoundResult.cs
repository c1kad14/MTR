namespace MTR.Domain;

public record RoundResult : IBaseEntity
{
    public Round Round { get; set; }
    public Player Player { get; set; }
    public int Score { get; set; }
    public DateTime ModifiedDate { get; set; }
}
