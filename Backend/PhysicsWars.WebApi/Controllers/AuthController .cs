using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PhysicsWars.Application.Features.Auth.Authentication;
using PhysicsWars.Application.Features.Auth.Authentication.Commands;
using PhysicsWars.Application.Features.Auth.Authentication.Models;
using PhysicsWars.Application.Features.Auth.Registration.Commands;
using PhysicsWars.Application.Features.Auth.Registration.Models;

namespace PhysicsWars.WebApi.Controllers;

[ApiVersion("1.0")]
[AllowAnonymous]
public class AuthController : ApiControllerBase
{
    private readonly IStringLocalizer<AuthController> _localizer;

    public AuthController(IStringLocalizer<AuthController> localizer)
    {
        _localizer = localizer;
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Register([FromBody] UserForRegistrationModel request)
    {
        await Mediator.Send(new RegistrationCommand(request));
        return Ok(_localizer.GetString("CheckConfirmationEmail").Value);
    }

    [HttpGet("registration/complete")]
    public async Task<IActionResult> CompleteRegistration([FromQuery] Guid token)
    {
        var result = await Mediator.Send(new CompleteRegistrationCommand(token));
        return result.Match<IActionResult>(
            userFullDto => Ok(userFullDto),
            error => BadRequest(error)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserForLoginModel request)
    {
        var result = await Mediator.Send(new AuthenticationCommand(request));
        return result.Match<IActionResult>(
            userFullDto => Ok(userFullDto),
            error => BadRequest(error)
        );
    }
}