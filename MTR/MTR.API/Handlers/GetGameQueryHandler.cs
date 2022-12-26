﻿using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using MTR.API.Models;
using MTR.API.Queries;
using MTR.DAL;

namespace MTR.API.Handlers
{
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
            if (request.Guid == default)
            {
                return new Response<GameDto> { Message = "Game id is invalid." };
            }

            var game = await _context.Games.Include(g => g.Players)
                                           .ThenInclude(p => p.User)
                                           .SingleOrDefaultAsync(g => g.Guid == request.Guid);

            if (game is null)
            {
                return new Response<GameDto> { Message = "Game not found" };
            }

            var gameDto = _mapper.Map<GameDto>(game);

            return new Response<GameDto> { Success = true, Model = gameDto };
        }
    }
}
