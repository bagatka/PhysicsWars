namespace PhysicsWars.Application.Features.Notifications.Push;

public interface IPushNotificationService
{
    public void Send(IPushNotification emailNotification);
    public void Send(List<IPushNotification> personalizedEmailNotifications);
}