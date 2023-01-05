using MediatR;

using MTR.DTO;
using MTR.Web.Shared.Models;

using System.ComponentModel.DataAnnotations;

namespace MTR.Web.Shared.Commands;

public record SignInUserCommand : IRequest<Response<UserDto>>
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}
