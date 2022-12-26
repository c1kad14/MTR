namespace MTR.Domain;

public record RoundCard : IBaseEntity
{
    public Card Card { get; set; }
    public Round Round { get; set; }
    public DateTimeOffset ModifiedDate { get; set; }
}