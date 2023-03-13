using MediatR;

using MTR.DTO;
using MTR.Web.Shared.Models;

namespace MTR.Web.Shared.Queries;

public record GetCurrentRoundQuery(Guid Guid) : IRequest<Response<RoundDto>>;
