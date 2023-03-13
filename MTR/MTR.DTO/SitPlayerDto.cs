namespace MTR.DTO;

public record SitPlayerDto : IDto
{
    public Guid Guid { get; set; }
    public Guid PlayerGuid { get; set; }
    public int Position { get; set; }
}
