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

public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, ResponseMultiple<GameDto>>
{
    private readonly IMapper _mapper;
    private readonly MTRContext _context;

    public GetGamesQueryHandler(IMapper mapper, MTRContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ResponseMultiple<GameDto>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        var gameDtos = new List<GameDto>();
        var pageSize = 10;
        var skip = pageSize * (request.Page - 1);

        try
        {
            var games = await _context.Games.Include(g => g.Players)
                                           .ThenInclude(p => p.MTRUser)
                                           .Include(g => g.Status)
                                           .Where(g => request.Status.Contains(g.Status.OrderBy(s => s.Modified).Last().Status))
                                           .Skip(skip)
                                           .Take(pageSize)
                                           .ToListAsync();

            foreach (var game in games)
            {
                var gameDto = _mapper.Map<GameDto>(game);
                gameDtos.Add(gameDto);
            }
        }
        catch (Exception ex)
        {
            return new ResponseMultiple<GameDto> { Message = ex.Message };
        }

        return new ResponseMultiple<GameDto> { Success = true, Model = gameDtos.ToArray() };
    }
}
