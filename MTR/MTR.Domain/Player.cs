using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record Player : IEntity
{
    public Guid Guid { get; set; }
    public Game Game { get; set; }
    public int GameId { get; set; }
    public MTRUser MTRUser { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
    public Guid MTRUserId { get; set; }
    public List<PlayerPosition> Position { get; set; }
    public List<RoundResult> Results { get; set; }
    public List<PlayerRemoved> Removed { get; set; } = new();
}
