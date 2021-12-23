using OneOf;
using OneOf.Types;
using PhysicsWars.Application.Common.DTOs.User;
using PhysicsWars.Application.Features.Auth.Registration.Services;

namespace PhysicsWars.Application.Features.Auth.Registration.Commands;

public sealed record CompleteRegistrationCommand(Guid Id) : ICommandBase<UserFullDto>;

internal sealed class CompleteRegistrationCommandHandler
    : ICommandHandlerBase<CompleteRegistrationCommand, UserFullDto>
{
    private readonly IRegistrationService _registrationService;

    public CompleteRegistrationCommandHandler(IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }

    public async Task<OneOf<UserFullDto, Error<string>>> Handle(
        CompleteRegistrationCommand request,
        CancellationToken cancellationToken
    )
    {
        return await _registrationService.CompleteRegistrationAsync(request.Id);
    }
}