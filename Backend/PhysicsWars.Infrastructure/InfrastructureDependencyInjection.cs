using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhysicsWars.Application.Common.DataAccess;
using PhysicsWars.Infrastructure.DataAccess;

namespace PhysicsWars.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPhysicsWarsDatabase(configuration);

        return services;
    }

    private static void AddPhysicsWarsDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PhysicsWarsDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("PhysicsWars"))
        );

        services.AddScoped<IPhysicsWarsDatabase, PhysicsWarsDatabase>();
    }
}