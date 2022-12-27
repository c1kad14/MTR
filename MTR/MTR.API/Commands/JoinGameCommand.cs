using MediatR;

using MTR.API.Models;
using MTR.DTO;

namespace MTR.API.Commands;

public class JoinGameCommand : IRequest<Response<GameDto>>
{
    public Guid Guid { get; set; }
    public Guid UserGuid { get; set; }
}
