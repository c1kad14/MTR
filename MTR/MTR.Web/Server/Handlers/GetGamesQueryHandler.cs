using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using MTR.Web.Shared.Models;
using MTR.Web.Shared.Queries;
using MTR.DAL;
using MTR.DTO;
using System.Linq.Expressions;
using MTR.Domain;
using System.Collections.Generic;

namespace MTR.Web.Shared.Handlers;

public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, ResponseMultiple<GameListItemDto>>
{
    private readonly IMapper _mapper;
    private readonly MTRContext _context;

    public GetGamesQueryHandler(IMapper mapper, MTRContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ResponseMultiple<GameListItemDto>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        var gameDtos = new GameListItemDto[] { };
        var pageSize = 100;
        var skip = pageSize * (request.Page - 1);
        var statuses = _mapper.Map<List<StatusType>>(request.Status);

        try
        {
            var games = await _context.Games.Include(g => g.Players)
                                           .ThenInclude(p => p.MTRUser)
                                           .Include(g => g.Status)
                                           .Where(g => statuses.Contains(g.Status.OrderBy(s => s.Modified).Last().Status))
                                           .Skip(skip)
                                           .Take(pageSize)
                                           .OrderByDescending(g => g.Created)
                                           .ToListAsync();

            gameDtos = _mapper.Map<GameListItemDto[]>(games);
        }
        catch (Exception ex)
        {
            return new ResponseMultiple<GameListItemDto> { Message = ex.Message };
        }

        return new ResponseMultiple<GameListItemDto> { Success = true, Model = gameDtos.ToArray() };
    }
}
