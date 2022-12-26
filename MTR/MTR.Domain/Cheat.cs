namespace MTR.Domain;

public record Cheat : IBaseEntity
{
    public Action Action { get; set; }
    public TurnCard Card { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
    public bool IsAccounted { get; set; }
}
