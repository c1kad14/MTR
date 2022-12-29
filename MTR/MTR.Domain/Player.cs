using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record Player : IEntity
{
    public Game Game { get; set; }
    public int GameId { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public List<PlayerPosition> Position { get; set; }
    public List<RoundResult> Results { get; set; }
    public List<PlayerRemoved> Removed { get; set; } = new();
}
