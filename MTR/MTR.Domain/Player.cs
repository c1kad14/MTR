namespace MTR.Domain;

public record Player : IBaseEntity
{
    public Game Game { get; set; }
    public User User { get; set; }
    public List<RoundCard> Cards { get; set; }
}
