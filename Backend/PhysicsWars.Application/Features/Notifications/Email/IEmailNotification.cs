namespace PhysicsWars.Application.Features.Notifications.Email;

public interface IEmailNotification
{
    string Subject { get; }
    string Body { get; }
    string RecipientEmail { get; }
}