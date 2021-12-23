namespace PhysicsWars.Domain.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }

    public byte[] Password { get; set; }
    public byte[] Salt { get; set; }

    public int Level { get; set; }
    public int Score { get; set; }

    public bool IsAdmin { get; set; }

    public bool IsBanned { get; set; }
    public DateTimeOffset? BannedUntil { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsTwoFactorEnabled { get; set; }
}