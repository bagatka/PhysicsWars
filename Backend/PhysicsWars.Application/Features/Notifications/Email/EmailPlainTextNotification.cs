namespace PhysicsWars.Application.Features.Notifications.Email;

public sealed record EmailPlainTextNotification(string Subject, string Body, string RecipientEmail) : IEmailNotification;