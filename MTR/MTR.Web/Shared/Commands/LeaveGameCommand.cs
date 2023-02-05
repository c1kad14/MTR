using MediatR;

using MTR.Web.Shared.Models;
using MTR.DTO;

namespace MTR.Web.Shared.Commands;

public record LeaveGameCommand : IRequest<Response<GameDto>>
{
    public Guid Guid { get; set; }
    public Guid PlayerGuid { get; set; }
}
