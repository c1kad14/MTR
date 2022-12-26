using MediatR;

using MTR.API.Models;

namespace MTR.API.Commands;

public class JoinGameCommand : IRequest<Response<GameDto>>
{
    public Guid Guid { get; set; }
    public Guid UserGuid { get; set; }
}
