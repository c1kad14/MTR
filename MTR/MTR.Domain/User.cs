using System.ComponentModel.DataAnnotations;

namespace MTR.Domain;

public record User : IBaseEntity
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    [MaxLength(16)]
    [Required]
    public string Name { get; set; }
    [MaxLength(32)]
    [Required]
    public string Email { get; set; }
    public Image Image { get; set; }
    [MaxLength(32)]
    [Required]
    public string Password { get; set; }
}
