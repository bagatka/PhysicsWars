using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhysicsWars.Application.Common.DataAccess;
using PhysicsWars.Application.Features.Auth.Password;
using PhysicsWars.Application.Features.Auth.Registration.Services;
using PhysicsWars.Application.Features.Notifications.Email.Services;
using PhysicsWars.Infrastructure.DataAccess;
using PhysicsWars.Infrastructure.Services.Auth;
using PhysicsWars.Infrastructure.Services.Notifications;

namespace PhysicsWars.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IPhysicsWarsDbContext, PhysicsWarsDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("PhysicsWars"))
        );

        services.AddLocalization(
            options =>
            {
                options.ResourcesPath = "Localization";
            }
        );
        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.SetDefaultCulture("en-Us");
            options.AddSupportedUICultures("en-US", "ru-RU");
        });

        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IEmailNotificationService, EmailNotificationService>();
        services.AddScoped<IRegistrationService, RegistrationService>();

        return services;
    }
}