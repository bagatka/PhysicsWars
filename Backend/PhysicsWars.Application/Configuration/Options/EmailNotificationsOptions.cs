namespace PhysicsWars.Application.Configuration.Options;

public class EmailNotificationsOptions
{
    public const string SectionName = "EmailNotifications";

    public bool Enabled { get; set; } = false;
    public SmtpOptions Smtp { get; set; } = new();
}

public class SmtpOptions
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public bool UseDefaultCredentials { get; set; } = false;
    public SmtpCredentialsOptions Credentials { get; set; } = new();
    public bool EnableSsl { get; set; } = true;
}

public class SmtpCredentialsOptions
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenderName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}