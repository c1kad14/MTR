namespace MTR.DTO;

public class CardDto : IDto
{
    public Guid Guid { get; set; }
    public string Rank { get; set; }
    public string Suit { get; set; }
}
