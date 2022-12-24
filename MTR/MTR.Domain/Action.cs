namespace MTR.Domain;

public record Action : IBaseEntity
{
    public Turn Turn { get; set; }
    public Player Player { get; set; }
    public ActionType ActionType { get; set; }
    public List<TurnCard> Cards { get; set; }
    public List<Cheat> Cheats { get; set; }
}
