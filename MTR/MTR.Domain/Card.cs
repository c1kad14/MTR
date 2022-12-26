using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record Card
{
    [Key]
    public int Id { get; set; }
    public Rank Rank { get; set; }
    public Suit Suit { get; set; }
}
