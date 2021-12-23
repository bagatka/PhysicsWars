namespace PhysicsWars.Application.Features.Auth.Registration.Entities;

public class UserForRegisterEntity
{
    public Guid Id { get; set; }
    public string Email  { get; set; }
    public string Username  { get; set; }
    public byte[] Password  { get; set; }
    public byte[] Salt  { get; set; }
    public DateTimeOffset CreatedAt  { get; set; }
}