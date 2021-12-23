using System.Security.Cryptography;
using PhysicsWars.Application.Features.Auth.Password;

namespace PhysicsWars.Infrastructure.Services.Auth;

public class PasswordService : IPasswordService
{
    public byte[] GenerateSalt()
    {
        var salt = new byte[256];

        using var random = RandomNumberGenerator.Create();
        random.GetBytes(salt);

        return salt;
    }

    public byte[] HashPassword(string password, byte[] salt)
    {
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
        return pbkdf2.GetBytes(512);
    }

    public bool VerifyPassword(string password, byte[] savedPasswordSalt, byte[] savedPasswordHash)
    {
        var hash = HashPassword(password, savedPasswordSalt);
        return savedPasswordHash.SequenceEqual(hash);
    }
}