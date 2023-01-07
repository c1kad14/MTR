using MediatR;

using MTR.Web.Shared.Models;
using MTR.DTO;

namespace MTR.Web.Shared.Commands;

public record JoinGameCommand : IRequest<Response<GameDto>>
{
    public Guid Guid { get; set; }
    public Guid UserGuid { get; set; }
    public Guid PlayerGuid { get; set; }
    public string? TableType { get; set; }
}
