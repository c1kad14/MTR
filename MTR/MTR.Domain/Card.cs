using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record Card : IEntity
{
    public Rank Rank { get; set; }
    public Suit Suit { get; set; }
}
