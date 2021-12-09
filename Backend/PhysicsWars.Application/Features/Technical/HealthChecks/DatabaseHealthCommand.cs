using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PhysicsWars.Application.Common.DataAccess;

namespace PhysicsWars.Application.Features.Technical.HealthChecks;

public sealed record DatabaseHealthCommand : IRequest<string>;

internal sealed class DatabaseHealthHandler : IRequestHandler<DatabaseHealthCommand, string>
{
    private readonly IPhysicsWarsDatabase _database;

    public DatabaseHealthHandler(IPhysicsWarsDatabase database)
    {
        _database = database;
    }

    public async Task<string> Handle(DatabaseHealthCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _database.SaveAsync();
        }
        catch (Exception)
        {
            return "Database is not available.";
        }

        return "Database is up and running.";
    }
}