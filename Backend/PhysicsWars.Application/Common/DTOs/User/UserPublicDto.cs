namespace PhysicsWars.Application.Common.DTOs.User;

public class UserPublicDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }

    public int Level { get; set; }
    public int Score { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}