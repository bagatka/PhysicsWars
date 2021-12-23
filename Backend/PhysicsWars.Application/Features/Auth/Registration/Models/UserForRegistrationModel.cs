namespace PhysicsWars.Application.Features.Auth.Registration.Models;

public sealed record UserForRegistrationModel
{
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}