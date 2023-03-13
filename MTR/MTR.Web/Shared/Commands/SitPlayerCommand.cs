using MediatR;

using MTR.DTO;
using MTR.Web.Shared.Models;

namespace MTR.Web.Shared.Commands;

public record SitPlayerCommand(Guid Guid, Guid PlayerGuid, int Position) : IRequest<Response<SitPlayerDto>>;
