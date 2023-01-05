using MediatR;

using MTR.Web.Shared.Models;
using MTR.DTO;

namespace MTR.Web.Shared.Queries;

public record GetGameQuery(Guid Guid) : IRequest<Response<GameDto>>;