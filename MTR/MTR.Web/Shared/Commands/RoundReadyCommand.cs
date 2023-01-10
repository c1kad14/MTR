using MediatR;

using MTR.DTO;
using MTR.Web.Shared.Models;

using System.ComponentModel.DataAnnotations;

namespace MTR.Web.Shared.Commands;

public record RoundReadyCommand : IRequest<Response<EmptyDto>>
{
    [Required]
    public Guid RoundGuid { get; set; }
    public Guid UserGuid { get; set; }
}
