using MediatR;

using MTR.Web.Shared.Models;
using MTR.DTO;

using System.ComponentModel.DataAnnotations;

namespace MTR.Web.Shared.Commands;

public record BeginRoundCommand : IRequest<Response<RoundDto>>
{
    [Required]
    public Guid GameGuid { get; set; }
    [Required]
    public Guid RoundGuid { get; set; }
}
