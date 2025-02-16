using FluentValidation;

namespace CloudStore.Application.Features.Users.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email not valid.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .NotNull().WithMessage("Password is null.")
            .MaximumLength(50).WithMessage("Password length exceeded.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .NotNull().WithMessage("First name is null.")
            .MaximumLength(50).WithMessage("First name length exceeded.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .NotNull().WithMessage("Last name is null.")
            .MaximumLength(50).WithMessage("Last name length exceeded.");
    }
}