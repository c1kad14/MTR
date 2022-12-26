namespace MTR.Domain;

public record Player
{
    public int Id { get; set; }
    public Game Game { get; set; }
    public int GameId { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public List<RoundResult> Results { get; set; }
    public List<PlayerRemoved> Removed { get; set; } = new();
}
