namespace MTR.Domain;

public record TurnCard : IBaseEntity
{
    public Action Action { get; set; }
    public RoundCard CardToHit { get; set; }
    public RoundCard? HittingCard { get; set; }
    public bool IsCheat { get; set; }
    public List<Cheat> Cheats { get; set; }
}
