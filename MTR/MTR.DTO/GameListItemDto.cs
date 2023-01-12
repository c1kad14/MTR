namespace MTR.DTO;

public record GameListItemDto : IDto
{
    public Guid GameGuid { get; set; }
    public string Owner { get; set; }
    public string Type { get; set; }
    public string PlayersInfo { get; set; }
}
