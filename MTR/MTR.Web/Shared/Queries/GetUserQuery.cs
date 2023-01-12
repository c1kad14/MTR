using MediatR;

using MTR.DTO;

namespace MTR.Web.Shared.Queries;

public record GetPlayerQuery : IRequest<PlayerDto>
{
    public Guid GameGuid { get; set; }
    public string Username { get; set; }
}
