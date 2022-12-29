namespace MTR.Domain;

public record PlayerPosition : IEntity
{
    public Guid Guid { get; set; }
    public Player Player { get; set; }
    public int PlayerId { get; set; }
    public int Position { get; set; }
}
