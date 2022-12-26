namespace MTR.Domain;

public record Turn
{
    public int Id { get; set; }
    public Player Player { get; set; }
    public Player OppositePlayer { get; set; }
    public Round Round { get; set; }
}
