namespace PhysicsWars.Application.Features.Auth.Authentication.Models;

public class UserForLoginModel
{
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}