using MediatR;
using OneOf;
using OneOf.Types;
using PhysicsWars.Application.Features.Auth.Registration.Models;
using PhysicsWars.Application.Features.Auth.Registration.Services;

namespace PhysicsWars.Application.Features.Auth.Registration.Commands;

public sealed record RegistrationCommand(UserForRegistrationModel RegistrationModel) : ICommandBase;

internal sealed class RegistrationHandler : ICommandHandlerBase<RegistrationCommand>
{
    private readonly IRegistrationService _registrationService;

    public RegistrationHandler(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    public async Task<OneOf<Unit, Error<string>>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        await _registrationService.RegisterAsync(request.RegistrationModel);
        return Unit.Value;
    }
}