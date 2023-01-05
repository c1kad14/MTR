using MediatR;

using MTR.Web.Shared.Models;
using MTR.DTO;

using System.ComponentModel.DataAnnotations;

namespace MTR.Web.Shared.Commands;

public record SignUpUserCommand : IRequest<Response<UserDto>>
{
    public Guid Guid { get; set; }

    [Required]
    [MaxLength(16)]
    [StringLength(16, ErrorMessage = "Must be between 2 and 16 characters", MinimumLength = 2)]
    public string Username { get; set; }

    [Required]
    [MaxLength(32)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(32, ErrorMessage = "Must be between 6 and 32 characters", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string Confirm { get; set; }

    public int Image { get; set; }
}
