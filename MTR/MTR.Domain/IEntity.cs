using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record IEntity
{
    [Key]
    public int Id { get; set; }
}
