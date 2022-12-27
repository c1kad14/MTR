namespace MTR.DTO;

public record GameDto : IDto
{
    public Guid Guid { get; set; }
    public List<PlayerDto> Players { get; set; }
}
