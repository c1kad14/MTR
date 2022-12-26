using MTR.Domain;

namespace MTR.API.Models;

public record GameDto : IDto
{
    public Guid Guid { get; set; }
    public List<PlayerDto> Players { get; set; }
}
