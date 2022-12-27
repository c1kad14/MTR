using MediatR;

using MTR.API.Models;
using MTR.DTO;

using System.ComponentModel.DataAnnotations;

namespace MTR.API.Commands;

public class BeginRoundCommand : IRequest<Response<RoundDto>>
{
    [Required]
    public Guid GameGuid { get; set; }
    [Required]
    public Guid RoundGuid { get; set; }
}
