using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record Cheat : IEntity
{
    public Action Action { get; set; }
    public int ActionId { get; set; }
    public TurnCard Card { get; set; }
    public int CardId { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
    public bool IsAccounted { get; set; }
}
