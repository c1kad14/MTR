namespace MTR.Domain;

public record PlayerRemoved
{
    public int Id { get; set; }
    public Player Player { get; set; }
    public int PlayerId { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
