using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using MTR.DAL;
using MTR.DTO;
using MTR.Web.Shared.Models;
using MTR.Web.Shared.Queries;

namespace MTR.Web.Server.Handlers;

public class GetCurrentRoundQueryHandler : IRequestHandler<GetCurrentRoundQuery, Response<RoundDto>>
{
    private readonly IMapper _mapper;
    private readonly MTRContext _context;

    public GetCurrentRoundQueryHandler(IMapper mapper, MTRContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Response<RoundDto>> Handle(GetCurrentRoundQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Guid == default)
            {
                return new Response<RoundDto> { Message = "Round id is invalid." };
            }

            var round = await _context.Rounds.Include(r => r.StartPlayer)
                               .Include(r => r.RoundReady)
                               .Include(r => r.RoundCards)
                               .ThenInclude(rc => rc.PlayerCards)
                               .Include(r => r.RoundCards)
                               .ThenInclude(rc => rc.MuckedCards)
                               .Include(r => r.Status)
                               .Include(r => r.Turns)
                               .Include(r => r.Game)
                               .ThenInclude(g => g.Players)
                               .ThenInclude(p => p.Removed)
                               .SingleOrDefaultAsync(r => r.Guid == request.Guid);

            if (round is null)
            {
                return new Response<RoundDto> { Message = "Round not found." };
            }

            if (round.Status.Any(s => s.Status == Domain.StatusType.Completed))
            {
                return new Response<RoundDto> { Message = "Round compled." };
            }

            if (round.Status.All(s => s.Status != Domain.StatusType.Running))
            {
                return new Response<RoundDto> { Message = "Round not started." };
            }

            round.Game.Players = round.Game.Players.Where(p => !p.Removed.Any()).ToList();

            return new Response<RoundDto> { Success = true };
        }
        catch (Exception ex)
        {
            return new Response<RoundDto> { Message = ex.Message };
        }
    }
}
