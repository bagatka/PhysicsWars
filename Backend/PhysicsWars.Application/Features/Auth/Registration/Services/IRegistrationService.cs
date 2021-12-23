using OneOf;
using OneOf.Types;
using PhysicsWars.Application.Common.DTOs.User;
using PhysicsWars.Application.Features.Auth.Registration.Models;

namespace PhysicsWars.Application.Features.Auth.Registration.Services;

public interface IRegistrationService
{
    Task<OneOf<Success, Error<string>>> RegisterAsync(UserForRegistrationModel userForRegistration);
    Task<OneOf<UserFullDto, Error<string>>> CompleteRegistrationAsync(Guid id);
}