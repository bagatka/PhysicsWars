using OneOf;
using OneOf.Types;
using PhysicsWars.Application.Features.Notifications.Email.Services;

namespace PhysicsWars.Application.Features.Notifications.Email.Commands;

public sealed record SendEmailCommand(IEmailNotification EmailNotification) : ICommandBase<string>;

internal sealed class SendEmailHandler : ICommandHandlerBase<SendEmailCommand, string>
{
    private readonly IEmailNotificationService _emailNotificationService;

    public SendEmailHandler(IEmailNotificationService emailNotificationService)
    {
        _emailNotificationService = emailNotificationService;
    }
    
    public async Task<OneOf<string, Error<string>>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        await _emailNotificationService.SendAsync(request.EmailNotification);
        return "Email sent";
    }
}