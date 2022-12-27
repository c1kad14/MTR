using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record MuckedCard : IEntity
{
    public RoundCard Card { get; set; }
    public int CardId { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
