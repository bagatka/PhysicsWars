using Microsoft.Extensions.Localization;
using OneOf;
using OneOf.Types;
using PhysicsWars.Application.Features.Auth.Authentication.Models;
using PhysicsWars.Application.Features.Auth.Authentication.Services;

namespace PhysicsWars.Application.Features.Auth.Authentication.Commands;

public sealed record AuthenticationCommand(UserForLoginModel UserForLogin) : ICommandBase<JwtToken>;

internal sealed class AuthenticationHandler : ICommandHandlerBase<AuthenticationCommand, JwtToken>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IStringLocalizer<AuthenticationCommand> _localizer;

    public AuthenticationHandler(
        IAuthenticationService authenticationService,
        IStringLocalizer<AuthenticationCommand> localizer
    )
    {
        _authenticationService = authenticationService;
        _localizer = localizer;
    }
    
    public async Task<OneOf<JwtToken, Error<string>>> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
    {
        var token = await _authenticationService.AuthenticateAsync(request.UserForLogin);

        if (token == null)
        {
            return new Error<string>(_localizer.GetString("InvalidCredentials"));
        }

        return token;
    }
}