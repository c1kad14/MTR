using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record MuckedCard : IEntity
{
    public Action Action { get; set; }
    public int ActionId { get; set; }
    public RoundCard RoundCard { get; set; }
    public int RoundCardId { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
