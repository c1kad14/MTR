using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record UserDetail : IEntity
{
    public Guid Guid { get; set; }
    public User User { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public Image Image { get; set; }

    public int? ImageId { get; set; }

    public string Password { get; set; }

    public DateTime Modified { get; set; } = DateTime.UtcNow;
}
