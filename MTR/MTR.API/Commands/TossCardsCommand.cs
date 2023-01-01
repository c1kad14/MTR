using MediatR;

using MTR.API.Models;
using MTR.DTO;

using System.ComponentModel.DataAnnotations;

namespace MTR.API.Commands;

public class TossCardsCommand : IRequest<Response<ActionDto>>
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
