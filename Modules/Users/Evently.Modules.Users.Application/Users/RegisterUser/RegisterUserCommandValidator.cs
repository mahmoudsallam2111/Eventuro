using FluentValidation;

namespace Evently.Modules.Users.Application.Users.RegisterUser;
internal sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.firstName).NotEmpty();
        RuleFor(c => c.lastName).NotEmpty();
        RuleFor(c => c.email).EmailAddress();
    }
}
