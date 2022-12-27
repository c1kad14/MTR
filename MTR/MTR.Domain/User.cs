using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTR.Domain;

public record User : IEntity
{
    public Guid Guid { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public List<UserDetail> Details { get; set; }
}
