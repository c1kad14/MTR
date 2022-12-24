namespace MTR.Domain;

public record Cheat : IBaseEntity
{
    public Action Action { get; set; }
    public TurnCard CheatCard { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool IsAccounted { get; set; }
}
