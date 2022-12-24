namespace MTR.Domain;

public record Card : IBaseEntity
{
    public Rank Rank { get; set; }
    public Suit Suit { get; set; }
}
