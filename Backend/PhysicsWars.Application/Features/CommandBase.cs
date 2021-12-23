using MediatR;
using OneOf;
using OneOf.Types;

namespace PhysicsWars.Application.Features;

public interface ICommandBase<TResult> : IRequest<OneOf<TResult, Error<string>>> {}
public interface ICommandBase : IRequest<OneOf<Unit, Error<string>>> {}

public interface ICommandHandlerBase<in TCommand, TResult> : IRequestHandler<TCommand, OneOf<TResult, Error<string>>>
    where TCommand : IRequest<OneOf<TResult, Error<string>>> {}

public interface ICommandHandlerBase<in TCommand> : IRequestHandler<TCommand, OneOf<Unit, Error<string>>>
    where TCommand : IRequest<OneOf<Unit, Error<string>>> {}