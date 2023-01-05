using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using MTR.Domain;
using MTR.Web.Shared.Commands;
using MTR.Web.Shared.Models;

namespace MTR.Web.Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<MTRUser> _signInManager;
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator, SignInManager<MTRUser> signInManager)
    {
        _mediator = mediator;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(SignInUserCommand command)
    {
        var response = await _mediator.Send(command);
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
        return Ok(response.Success);
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpUserCommand command)
    {
        var response = await _mediator.Send(command);
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
        return Ok(response.Model);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [HttpGet]
    public UserInfo UserInfo()
    {
        var response = new UserInfo
        {
            IsAuthenticated = User.Identity.IsAuthenticated,
            Username = User.Identity.Name,
            ExposedClaims = User.Claims.ToDictionary(c => c.Type, c => c.Value)
        };
        return response;
    }
}
