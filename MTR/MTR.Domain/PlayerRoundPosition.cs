namespace MTR.Domain;

public record PlayerRoundPosition : IEntity
{
    public Round Round { get; set; }
    public int RoundId { get; set; }
    public Player Player { get; set; }
    public int PlayerId { get; set; }
}
