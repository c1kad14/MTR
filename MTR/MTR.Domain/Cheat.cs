using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record Cheat : IEntity
{
    public TurnCard TurnCard { get; set; }
    public int TurnCardId { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
    public bool IsAccounted { get; set; }
}
