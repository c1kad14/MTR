namespace MTR.Domain;

public record IBaseEntity
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
}
