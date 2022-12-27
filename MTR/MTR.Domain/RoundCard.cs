using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record RoundCard : IEntity
{
    public Card Card { get; set; }
    public int CardId { get; set; }
    public Round Round { get; set; }
    public int RoundId { get; set; }
    public DateTime Modified { get; set; }
    public List<PlayerCard> PlayerCards { get; set; }
    public List<MuckedCard> MuckedCards { get; set; }
}