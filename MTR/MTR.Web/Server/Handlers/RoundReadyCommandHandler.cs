using MediatR;

using Microsoft.EntityFrameworkCore;

using MTR.DAL;
using MTR.Domain;
using MTR.DTO;
using MTR.Web.Shared.Commands;
using MTR.Web.Shared.Models;

namespace MTR.Web.Server.Handlers;

public class RoundReadyCommandHandler : IRequestHandler<RoundReadyCommand, Response<EmptyDto>>
{
    private readonly MTRContext _context;

    public RoundReadyCommandHandler(MTRContext context)
    {
        _context = context;
    }

    public async Task<Response<EmptyDto>> Handle(RoundReadyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var round = _context.Rounds
                .Include(r => r.RoundReady)
                .SingleOrDefault(r => r.Guid == request.RoundGuid);

            if (round is null)
            {
                return new Response<EmptyDto>() { Message = "Invalid round." };
            }

            var user = await _context.Users.Include(u => u.Details).SingleOrDefaultAsync(u => u.Guid == request.UserGuid);

            if (user is null)
            {
                return new Response<EmptyDto> { Message = "Invalid user." };
            }

            var player = _context.Players
                .Include(p => p.Removed)
                .SingleOrDefault(p => p.MTRUserId == user.Id && p.GameId == round.GameId);

            if (player is null)
            {
                return new Response<EmptyDto> { Message = "Invalid player." };
            }

            var roundReady = round.RoundReady
                .Where(rr => rr.PlayerId == player.Id)
                .OrderByDescending(rr => rr.Modified)
                .FirstOrDefault();

            if (roundReady is null || !roundReady.Ready)
            {
                _context.RoundReady.Add(new RoundReady
                {
                    PlayerId = player.Id,
                    RoundId = round.Id,
                    Ready = true
                });
                await _context.SaveChangesAsync();
            }

            return new Response<EmptyDto> { Success = true, Model = new(true) };
        }
        catch (Exception ex)
        {
            return new Response<EmptyDto> { Message = ex.Message };
        }
    }
}
