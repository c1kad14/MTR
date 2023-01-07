using System.ComponentModel.DataAnnotations;

namespace MTR.DTO;

public record GameDto : IDto
{
    public Guid Guid { get; set; }
    public string TableType { get; set; }
    [Range(3, 6)]
    public int MaxPlayers { get; set; }
    public List<PlayerDto> Players { get; set; } = new();
}
