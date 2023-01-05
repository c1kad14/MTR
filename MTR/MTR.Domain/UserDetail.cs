using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record UserDetail : IEntity
{
    public Guid Guid { get; set; }

    public MTRUser MTRUser { get; set; }

    public Guid MTRUserId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public Image Image { get; set; }

    public int? ImageId { get; set; }

    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
