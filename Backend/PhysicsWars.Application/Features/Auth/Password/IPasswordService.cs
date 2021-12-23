namespace PhysicsWars.Application.Features.Auth.Password;

public interface IPasswordService
{
    byte[] GenerateSalt();
    byte[] HashPassword(string password, byte[] salt);
    bool VerifyPassword(string password, byte[] savedPasswordSalt, byte[] savedPasswordHash);
}