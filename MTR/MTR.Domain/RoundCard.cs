using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record RoundCard : IEntity
{
    public Guid Guid { get; set; }
    public Card Card { get; set; }
    public int CardId { get; set; }
    public Round Round { get; set; }
    public int RoundId { get; set; }
    public DateTime Modified { get; set; } = DateTime.UtcNow;
    public List<TurnCard> TurnCards { get; set; } = new();
    public List<PlayerCard> PlayerCards { get; set; } = new();
    public List<MuckedCard> MuckedCards { get; set; } = new();
}