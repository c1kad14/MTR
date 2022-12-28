using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record PlayerCard : IEntity
{
    public RoundCard RoundCard { get; set; }
    public int RoundCardId { get; set; }
    public Player Player { get; set; }
    public int PlayerId { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
