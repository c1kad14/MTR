using AutoMapper;

using IdentityModel.Client;

using MediatR;

using Microsoft.AspNetCore.Identity;

using MTR.DAL;
using MTR.Domain;
using MTR.DTO;
using MTR.Web.Shared.Commands;
using MTR.Web.Shared.Models;

namespace MTR.Web.Server.Handlers;

public class SignInUserCommandHandler : IRequestHandler<SignInUserCommand, Response<UserDto>>
{
    private readonly IMapper _mapper;
    private readonly MTRContext _context;
    private readonly UserManager<MTRUser> _userManager;
    private readonly SignInManager<MTRUser> _signInManager;

    public SignInUserCommandHandler(IMapper mapper, MTRContext context, UserManager<MTRUser> userManager, SignInManager<MTRUser> signInManager)
    {
        _mapper = mapper;
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Response<UserDto>> Handle(SignInUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(command.Username);
        if (user == null)
        {
            return new() { Message = "User does not exist." };
        }
        var singInResult = await _signInManager.CheckPasswordSignInAsync(user, command.Password, false);
        if (!singInResult.Succeeded)
        {
            return new() { Message = "Invalid password." };
        }

        await _signInManager.SignInAsync(user, command.RememberMe);
        var userDetails = _context.UserDetails.Where(ud => ud.MTRUserId == user.Id)
            .OrderByDescending(ud => ud.Modified)
            .First();
        userDetails.MTRUser = user;

        var userDto = _mapper.Map<UserDto>(userDetails);
        return new()
        {
            Success = true,
            Model = userDto
        };
    }
}
