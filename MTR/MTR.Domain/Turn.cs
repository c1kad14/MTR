namespace MTR.Domain;

public record Turn : IBaseEntity
{
    public Player Player { get; set; }
    public Round Round { get; set; }
}
