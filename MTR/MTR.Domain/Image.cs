namespace MTR.Domain;

public record Image : IBaseEntity
{
    public string Path { get; set; }
}
