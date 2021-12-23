using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using OneOf;
using OneOf.Types;
using PhysicsWars.Application.Common.DataAccess;
using PhysicsWars.Application.Common.DTOs.User;
using PhysicsWars.Application.Configuration.Options;
using PhysicsWars.Application.Features.Auth.Password;
using PhysicsWars.Application.Features.Auth.Registration.Entities;
using PhysicsWars.Application.Features.Auth.Registration.Models;
using PhysicsWars.Application.Features.Auth.Registration.Services;
using PhysicsWars.Application.Features.Notifications.Email;
using PhysicsWars.Application.Features.Notifications.Email.Services;
using PhysicsWars.Domain.Entities;

namespace PhysicsWars.Infrastructure.Services.Auth;

public class RegistrationService : IRegistrationService
{
    private readonly IPhysicsWarsDbContext _database;
    private readonly IPasswordService _passwordService;
    private readonly IEmailNotificationService _emailNotificationService;
    private readonly IStringLocalizer<RegistrationService> _localizer;
    private RegistrationOptions _registrationOptions;

    public RegistrationService(
        IPhysicsWarsDbContext database,
        IEmailNotificationService emailNotificationService,
        IPasswordService passwordService,
        IOptionsMonitor<RegistrationOptions> registrationOptions,
        IStringLocalizer<RegistrationService> localizer
    )
    {
        _passwordService = passwordService;
        _localizer = localizer;
        _registrationOptions = registrationOptions.CurrentValue;
        _emailNotificationService = emailNotificationService;
        _database = database;

        registrationOptions.OnChange(
            options =>
            {
                _registrationOptions = options;
            }
        );
    }

    public async Task<OneOf<Success, Error<string>>> RegisterAsync(UserForRegistrationModel userForRegistration)
    {
        var sameUsernameUser = await _database.Users
            .FirstOrDefaultAsync(
                user => user.Username.Equals(userForRegistration.Username)
            );

        if (sameUsernameUser != null)
        {
            return new Error<string>(_localizer.GetString("UsernameAlreadyExistsError"));
        }

        var sameEmailUser = await _database.Users
            .FirstOrDefaultAsync(
                user => user.Email.Equals(userForRegistration.Email)
            );

        if (sameEmailUser != null)
        {
            await SendRedundantRegistrationAttemptEmail(sameEmailUser);
            return new Error<string>(_localizer.GetString("EmailAlreadyExistsError"));
        }

        var salt = _passwordService.GenerateSalt();
        var passwordHash = _passwordService.HashPassword(userForRegistration.Password, salt);

        var userForRegister = new UserForRegisterEntity
        {
            Id = Guid.NewGuid(),
            Email = userForRegistration.Email,
            Username = userForRegistration.Username,
            Password = passwordHash,
            Salt = salt,
            CreatedAt = DateTimeOffset.Now
        };

        _database.UsersToRegister.Add(userForRegister);
        await _database.SaveChangesAsync();

        await SendConfirmationEmail(userForRegister.Id, userForRegistration);

        return new Success();
    }

    public async Task<OneOf<UserFullDto, Error<string>>> CompleteRegistrationAsync(Guid id)
    {
        var userToRegister = await _database.UsersToRegister.FirstOrDefaultAsync(x => x.Id == id);

        if (userToRegister == null)
        {
            return new Error<string>(_localizer.GetString("IncorrectConfirmationLinkError"));
        }

        var user = new UserEntity
        {
            Email = userToRegister.Email,
            Username = userToRegister.Username,
            Password = userToRegister.Password,
            Salt = userToRegister.Salt,
            CreatedAt = DateTimeOffset.Now,
            Level = 0,
            Score = 0,
            IsAdmin = false,
            IsBanned = false,
            UpdatedAt = DateTimeOffset.Now,
            IsDeleted = false,
            IsTwoFactorEnabled = false
        };

        await _database.Users.AddAsync(user);
        _database.UsersToRegister.Remove(userToRegister);
        await _database.SaveChangesAsync();

        return user.Adapt<UserFullDto>();
    }

    private async Task SendConfirmationEmail(Guid tokenId, UserForRegistrationModel userForRegistration)
    {
        var verificationUrl = $"{_registrationOptions.EmailConfirmation.LinkBase}{tokenId}";

        var emailSubject = _localizer.GetString("EmailConfirmationSubject");
        var emailBody = _localizer.GetString("EmailConfirmationBody", userForRegistration.Username, verificationUrl);

        var emailNotification = new EmailPlainTextNotification(emailSubject, emailBody, userForRegistration.Email);

        await _emailNotificationService.SendAsync(emailNotification);
    }

    private async Task SendRedundantRegistrationAttemptEmail(UserEntity user)
    {
        var changePasswordUrl = $"{_registrationOptions.ResetPassword.LinkBase}?token={user.Id}";

        var emailSubject = _localizer.GetString("EmailRedundantRegistrationAttemptSubject");
        var emailBody = _localizer.GetString("EmailRedundantRegistrationAttemptBody", user.Username, changePasswordUrl);

        var emailNotification = new EmailPlainTextNotification(emailSubject, emailBody, user.Email);
        await _emailNotificationService.SendAsync(emailNotification);
    }
}