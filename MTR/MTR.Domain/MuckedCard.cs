namespace MTR.Domain;

public record MuckedCard
{
    public int Id { get; set; }
    public RoundCard Card { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
