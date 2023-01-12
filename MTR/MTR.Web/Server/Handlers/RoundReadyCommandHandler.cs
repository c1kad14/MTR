using MediatR;

using Microsoft.EntityFrameworkCore;

using MTR.DAL;
using MTR.Domain;
using MTR.DTO;
using MTR.Web.Shared.Commands;
using MTR.Web.Shared.Models;

namespace MTR.Web.Server.Handlers;

public class RoundReadyCommandHandler : IRequestHandler<RoundReadyCommand, Response<RoundReadyDto>>
{
    private readonly MTRContext _context;

    public RoundReadyCommandHandler(MTRContext context)
    {
        _context = context;
    }

    public async Task<Response<RoundReadyDto>> Handle(RoundReadyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var round = _context.Rounds
                .Include(r => r.RoundReady)
                .SingleOrDefault(r => r.Guid == request.RoundGuid);

            if (round is null)
            {
                return new Response<RoundReadyDto>() { Message = "Invalid round." };
            }

            var player = _context.Players
                .Include(p => p.Removed)
                .SingleOrDefault(p => p.Guid == request.PlayerGuid && p.GameId == round.GameId && !p.Removed.Any());

            if (player is null)
            {
                return new Response<RoundReadyDto> { Message = "Invalid player." };
            }

            var roundReady = round.RoundReady
                .Where(rr => rr.PlayerId == player.Id)
                .OrderByDescending(rr => rr.Modified)
                .FirstOrDefault();

            if (roundReady is null || roundReady.Ready != request.IsReady)
            {
                _context.RoundReady.Add(new RoundReady
                {
                    PlayerId = player.Id,
                    RoundId = round.Id,
                    Ready = request.IsReady
                });

                await _context.SaveChangesAsync();

                return new Response<RoundReadyDto> { Success = true, Model = new(request.IsReady) };
            }

            return new Response<RoundReadyDto>();
        }
        catch (Exception ex)
        {
            return new Response<RoundReadyDto> { Message = ex.Message };
        }
    }
}
