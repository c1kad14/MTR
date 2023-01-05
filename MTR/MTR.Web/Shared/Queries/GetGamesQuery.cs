using MediatR;

using MTR.Domain;
using MTR.DTO;
using MTR.Web.Shared.Models;

namespace MTR.Web.Shared.Queries;

public record GetGamesQuery(int Page, List<StatusType> Status) : IRequest<ResponseMultiple<GameDto>>;
