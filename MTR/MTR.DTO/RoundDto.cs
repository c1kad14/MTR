namespace MTR.DTO;

public record RoundDto : IDto
{
    public List<PlayerDto> Players { get; set; }

}
