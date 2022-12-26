using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTR.Domain;

public record User : IBaseEntity
{
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public List<UserDetail> Details { get; set; }
}
