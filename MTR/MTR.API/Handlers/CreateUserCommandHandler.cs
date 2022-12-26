using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using MTR.API.Commands;
using MTR.API.Models;
using MTR.DAL;
using MTR.Domain;

namespace MTR.API.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly MTRContext _context;

    public CreateUserCommandHandler(IMapper mapper, MTRContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Response<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (request.Guid == default)
        {
            return new Response<UserDto> { Message = "User id is invalid." };
        }

        var user = _context.Users
            .Include(u => u.Details)
            .ThenInclude(d => d.Image)
            .SingleOrDefault(u => u.Guid == request.Guid);

        if (user is null)
        {
            var message = string.Empty;
            var existing = _context.Users
                .Include(u => u.Details)
                .Where(u => u.Details.OrderByDescending(d => d.Modified).First().Name == request.Username
                || u.Details.OrderByDescending(d => d.Modified).First().Email == request.Email)
                .Select(d => d.Details.OrderByDescending(d => d.Modified).First())
                .ToList();

            if (existing.Any(u => u.Name.Equals(request.Username, StringComparison.InvariantCultureIgnoreCase)))
            {
                message = "A user with this name already exists. ";
            }
            else if (existing.Any(u => u.Email.Equals(request.Email, StringComparison.InvariantCultureIgnoreCase)))
            {
                message += "A user with this email already exists.";
            }

            if (!string.IsNullOrEmpty(message))
            {
                return new Response<UserDto> { Message = message };
            }

            user = _mapper.Map<User>(request);

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        var userDto = _mapper.Map<UserDto>(user.Details.OrderByDescending(d => d.Modified).First());

        return new Response<UserDto> { Success = true, Model = userDto };
    }
}
