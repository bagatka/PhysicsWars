namespace PhysicsWars.Application.Configuration.Options;

public class RegistrationOptions
{
    public const string SectionName = "Registration";

    public bool Enabled { get; set; }
    public RegistrationEmailConfirmationOptions EmailConfirmation { get; set; } = new();
    public RegistrationResetPasswordOptions ResetPassword { get; set; } = new();
}

public class RegistrationEmailConfirmationOptions
{
    public string LinkBase { get; set; } = string.Empty;
}
public class RegistrationResetPasswordOptions
{
    public string LinkBase { get; set; } = string.Empty;
}