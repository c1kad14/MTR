using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using MTR.Web.Shared.Models;
using MTR.Web.Shared.Queries;
using MTR.DAL;
using MTR.DTO;
using System.Linq.Expressions;

namespace MTR.Web.Shared.Handlers;

public class GetGameQueryHandler : IRequestHandler<GetGameQuery, Response<GameDto>>
{
    private readonly IMapper _mapper;
    private readonly MTRContext _context;

    public GetGameQueryHandler(IMapper mapper, MTRContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Response<GameDto>> Handle(GetGameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Guid == default)
            {
                return new Response<GameDto> { Message = "Game id is invalid." };
            }

            var game = await _context.Games.Include(g => g.Players)
                                           .ThenInclude(p => p.MTRUser)
                                           .Include(g => g.Players)
                                           .ThenInclude(p => p.RoundReady)
                                           .Include(g => g.Players)
                                           .ThenInclude(p => p.Removed)
                                           .SingleOrDefaultAsync(g => g.Guid == request.Guid);

            if (game is null)
            {
                return new Response<GameDto> { Message = "Game not found." };
            }

            game.Players = game.Players.Where(p => !p.Removed.Any()).ToList();

            var gameDto = _mapper.Map<GameDto>(game);
            return new Response<GameDto> { Success = true, Model = gameDto };
        }
        catch (Exception ex)
        {
            return new Response<GameDto> { Message = ex.Message };
        }
    }
}
