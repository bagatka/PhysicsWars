namespace PhysicsWars.Application.Features.Notifications.Push;

public interface IPushNotification
{
    string Header { get; set; }
    string Body { get; set; }
    Guid RecipientId { get; set; }
}