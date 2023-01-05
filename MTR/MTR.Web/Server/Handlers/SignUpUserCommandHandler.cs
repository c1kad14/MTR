using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;

using MTR.Web.Shared.Commands;
using MTR.Web.Shared.Models;
using MTR.DAL;
using MTR.Domain;
using MTR.DTO;
using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using IdentityModel.Client;

namespace MTR.API.Handlers;

public class SignUpUserCommandHandler : IRequestHandler<SignUpUserCommand, Response<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly MTRContext _context;
    private readonly UserManager<MTRUser> _userManager;

    public SignUpUserCommandHandler(IMapper mapper, MTRContext context, UserManager<MTRUser> userManager)
    {
        _mapper = mapper;
        _context = context;
        _userManager = userManager;
    }

    public async Task<Response<UserDto>> Handle(SignUpUserCommand command, CancellationToken cancellationToken)
    {
        if (command.Guid == default)
        {
            return new Response<UserDto> { Message = "User id is invalid." };
        }

        var user = _context.Users
            .Include(u => u.Details)
            .ThenInclude(d => d.Image)
            .SingleOrDefault(u => u.Guid == command.Guid);

        if (user is null)
        {
            user = _mapper.Map<MTRUser>(command);
            var result = await _userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                return new() { Message = result.Errors.FirstOrDefault()?.Description ?? "Failed to create user." };
            }
        }

        var userDto = _mapper.Map<UserDto>(user.Details.OrderByDescending(d => d.Modified).First());
        return new Response<UserDto> { Success = true, Model = userDto };
    }
}
