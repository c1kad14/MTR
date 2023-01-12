using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MTR.DAL;
using MTR.Domain;
using MTR.DTO;
using MTR.Web.Shared.Queries;

namespace MTR.Web.Server.Handlers;

public class GetPlayerQueryHandler : IRequestHandler<GetPlayerQuery, PlayerDto?>
{
    private readonly UserManager<MTRUser> _userManager;
    private readonly MTRContext _context;
    private readonly IMapper _mapper;

    public GetPlayerQueryHandler(UserManager<MTRUser> userManager, MTRContext mtrContext, IMapper mapper)
    {
        _userManager = userManager;
        _context = mtrContext;
        _mapper = mapper;
    }

    public async Task<PlayerDto?> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            var game = await _context.Games.SingleOrDefaultAsync(g => g.Guid == request.GameGuid);

            if (user is null || game is null)
            {
                return null;
            }

            var player = await _context.Players
                .Include(p => p.Removed)
                .Where(p => p.MTRUserId == user.Id && p.GameId == game.Id && !p.Removed.Any())
                .SingleOrDefaultAsync();

            return _mapper.Map<PlayerDto>(player);
        }
        catch
        {
            return null;
        }
    }
}
