namespace MTR.API.Models;

public record PlayerDto : IDto
{
    public Guid Guid { get; set; }
    public string Username { get; set; }
    public int? Score { get; set; }
}