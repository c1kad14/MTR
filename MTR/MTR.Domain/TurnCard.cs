namespace MTR.Domain;

public record TurnCard : IEntity
{
    public Turn Turn { get; set; }
    public int TurnId { get; set; }
    public Action Action { get; set; }
    public int ActionId { get; set; }
    public RoundCard RoundCard { get; set; }
    public int RoundCardId { get; set; }
    public TurnCard? OppositeTurnCard { get; set; }
    public int? OppositeTurnCardId { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
    public List<Cheat> Cheats { get; set; }
}
