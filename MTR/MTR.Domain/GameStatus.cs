namespace MTR.Domain;

public record GameStatus : IEntity
{
    public Game Game { get; set; }
    public int GameId { get; set; }
    public StatusType Status { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
