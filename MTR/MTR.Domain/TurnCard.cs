namespace MTR.Domain;

public record TurnCard : IEntity
{
    public Guid Guid { get; set; }
    public Action Action { get; set; }
    public int ActionId { get; set; }
    public RoundCard Card { get; set; }
    public int CardId { get; set; }
    public TurnCard? OppositeCard { get; set; }
    public int? OppositeCardId { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
