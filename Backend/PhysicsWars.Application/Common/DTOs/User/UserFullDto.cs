namespace PhysicsWars.Application.Common.DTOs.User;

public class UserFullDto : UserPublicDto
{
    public string Email { get; set; }

    public bool IsBanned { get; set; }
    public DateTimeOffset? BannedUntil { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsTwoFactorEnabled { get; set; }
}