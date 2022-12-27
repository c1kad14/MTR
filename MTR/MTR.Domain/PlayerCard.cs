using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record PlayerCard : IEntity
{
    public RoundCard Card { get; set; }
    public int CardId { get; set; }
    public Player Player { get; set; }
    public int PlayerId { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
