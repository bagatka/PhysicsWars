using PhysicsWars.Application.Features.Auth.Authentication.Models;
using PhysicsWars.Application.Features.Auth.Authentication.Services;

namespace PhysicsWars.Infrastructure.Services.Auth;

public class AuthenticationService : IAuthenticationService
{
    public Task<JwtToken?> AuthenticateAsync(UserForLoginModel userForLoginModel)
    {
        throw new NotImplementedException();
    }

    public Task<JwtToken?> TwoFactorAuthenticateAsync(UserForLoginModel userForLoginModel, string confirmationCode)
    {
        throw new NotImplementedException();
    }

    public Task SendTwoFactorCodeAsync(UserForLoginModel userForLoginModel)
    {
        throw new NotImplementedException();
    }
}