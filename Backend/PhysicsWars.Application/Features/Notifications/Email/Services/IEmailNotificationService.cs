namespace PhysicsWars.Application.Features.Notifications.Email.Services;

public interface IEmailNotificationService
{
    public Task SendAsync(IEmailNotification emailNotification);
    public Task SendAsync(List<IEmailNotification> emailNotifications);
}