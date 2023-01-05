using MediatR;

using MTR.Web.Shared.Models;
using MTR.DTO;

using System.ComponentModel.DataAnnotations;

namespace MTR.Web.Shared.Commands;

public record TossCardsCommand : IRequest<Response<ActionDto>>
{
    [Required]
    public Guid Guid { get; set; }
    [Required]
    public Guid RoundGuid { get; set; }
    [Required]
    public Guid PlayerGuid { get; set; }
    [Required]
    public List<CardDto> Cards { get; set; }
}
