using FluentValidation;
using PhysicsWars.Application.Common.Constants;
using PhysicsWars.Application.Features.Auth.Registration.Commands;

namespace PhysicsWars.Application.Features.Auth.Registration.Validation;

public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
{
    public RegistrationCommandValidator()
    {
        RuleFor(x => x.RegistrationModel.Email)
            .NotEmpty()
                .WithMessage("Email is required")
            .EmailAddress()
                .WithMessage("Incorrect email format");

        RuleFor(x => x.RegistrationModel.Password)
            .NotEmpty()
                .WithMessage("Password is required")
            .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long")
            .Matches(RegularExpressionConstants.Password)
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number and one special character");

        RuleFor(x => x.RegistrationModel.Username)
            .NotEmpty()
                .WithMessage("Username is required")
            .MinimumLength(3)
                .WithMessage("Username must be at least 3 characters long");
    }
}