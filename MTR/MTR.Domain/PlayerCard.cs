namespace MTR.Domain;

public record PlayerCard
{
    public int Id { get; set; }
    public RoundCard Card { get; set; }
    public Player Player { get; set; }
    public DateTime ModifiedDate { get; set; }
}
