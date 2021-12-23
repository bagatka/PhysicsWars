using PhysicsWars.Application.Features.Auth.Authentication.Models;

namespace PhysicsWars.Application.Features.Auth.Authentication.Services;

public interface IAuthenticationService
{
    Task<JwtToken?> AuthenticateAsync(UserForLoginModel userForLoginModel);
    Task<JwtToken?> TwoFactorAuthenticateAsync(UserForLoginModel userForLoginModel, string confirmationCode);
    Task SendTwoFactorCodeAsync(UserForLoginModel userForLoginModel);
}