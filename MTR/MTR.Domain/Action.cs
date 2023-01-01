namespace MTR.Domain;

public record Action : IEntity
{
    public Guid Guid { get; set; }
    public Turn Turn { get; set; }
    public int TurnId { get; set; }
    public Player Player { get; set; }
    public int PlayerId { get; set; }
    public ActionType ActionType { get; set; }
    public List<TurnCard> TurnCards { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
