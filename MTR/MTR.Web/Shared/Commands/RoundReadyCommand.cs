using MediatR;

using MTR.DTO;
using MTR.Web.Shared.Models;

using System.ComponentModel.DataAnnotations;

namespace MTR.Web.Shared.Commands;

public record RoundReadyCommand : IRequest<Response<RoundReadyDto>>
{
    [Required]
    public Guid RoundGuid { get; set; }

    [Required]
    public Guid PlayerGuid { get; set; }

    [Required]
    public bool IsReady { get; set; }
}
