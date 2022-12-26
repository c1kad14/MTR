using MediatR;

using MTR.API.Models;

namespace MTR.API.Queries;

public class GetGameQuery : IRequest<Response<GameDto>>
{
    public Guid Guid { get; set; }

    public GetGameQuery(Guid guid)
    {
        Guid = guid;
    }
}
