using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record MuckedCard : IEntity
{
    public RoundCard RoundCard { get; set; }
    public int RoundCardId { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
