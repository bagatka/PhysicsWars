using MediatR;

namespace PhysicsWars.Application.Features.Technical.HealthChecks;

public sealed record HealthStatusCommand : IRequest<string>;

internal sealed class HealthStatusHandler : IRequestHandler<HealthStatusCommand, string>
{
    public Task<string> Handle(HealthStatusCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult("Api is up and running.");
    }
}