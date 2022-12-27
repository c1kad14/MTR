using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record PlayerRemoved : IEntity
{
    public Player Player { get; set; }
    public int PlayerId { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
