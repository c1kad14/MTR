namespace MTR.Domain;

public record Image : IEntity
{
    public string Path { get; set; }
}
