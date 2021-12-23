using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using PhysicsWars.Application.Configuration.Options;
using PhysicsWars.Application.Features.Notifications.Email;
using PhysicsWars.Application.Features.Notifications.Email.Services;

namespace PhysicsWars.Infrastructure.Services.Notifications;

public class EmailNotificationService : IEmailNotificationService
{
    private SmtpClient _smtpClient;
    private MailAddress _smtpAddress;
    private bool _isEmailNotificationEnabled;

    public EmailNotificationService(IOptionsMonitor<EmailNotificationsOptions> emailOptions)
    {
        _isEmailNotificationEnabled = emailOptions.CurrentValue.Enabled;
        ConfigureSmtpClient(emailOptions.CurrentValue);

        emailOptions.OnChange(
            options =>
            {
                _isEmailNotificationEnabled = options.Enabled;

                if (_isEmailNotificationEnabled)
                {
                    _smtpClient?.Dispose();
                    ConfigureSmtpClient(options);
                }
            }
        );
    }

    public Task SendAsync(IEmailNotification emailNotification)
    {
        if (!_isEmailNotificationEnabled)
        {
            return Task.CompletedTask;
        }

        var recipientAddress = new MailAddress(emailNotification.RecipientEmail);

        var message = new MailMessage(_smtpAddress, recipientAddress)
        {
            Subject = emailNotification.Subject, Body = emailNotification.Body
        };

        _smtpClient.SendCompleted += (_, _) =>
        {
            message.Dispose();
        };

        return _smtpClient.SendMailAsync(message);
    }

    public Task SendAsync(List<IEmailNotification> emailNotifications)
    {
        if (!_isEmailNotificationEnabled)
        {
            return Task.CompletedTask;
        }

        var sendEmailTasks = new List<Task>();

        foreach (var emailNotification in emailNotifications)
        {
            sendEmailTasks.Add(SendAsync(emailNotification));
        }

        return sendEmailTasks.Count == 0 ? Task.CompletedTask : Task.WhenAll(sendEmailTasks);
    }

    private void ConfigureSmtpClient(EmailNotificationsOptions emailNotificationsOptions)
    {
        if (!emailNotificationsOptions.Enabled)
        {
            return;
        }

        _smtpClient = new SmtpClient
        {
            Host = emailNotificationsOptions.Smtp.Host,
            Port = emailNotificationsOptions.Smtp.Port,
            EnableSsl = emailNotificationsOptions.Smtp.EnableSsl,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = emailNotificationsOptions.Smtp.UseDefaultCredentials,
            Credentials = new NetworkCredential(
                emailNotificationsOptions.Smtp.Credentials.Username,
                emailNotificationsOptions.Smtp.Credentials.Password
            )
        };

        _smtpAddress = new MailAddress(
            emailNotificationsOptions.Smtp.Credentials.Email,
            emailNotificationsOptions.Smtp.Credentials.SenderName
        );
    }
}