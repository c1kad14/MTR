namespace MTR.DTO;

public record PlayerDto : IDto
{
    public Guid Guid { get; set; }
    public string Username { get; set; }
    public DateTime Modified { get; set; }
    public int? Score { get; set; }
    public bool IsReady { get; set; }
    public int CardCount { get; set; }
}