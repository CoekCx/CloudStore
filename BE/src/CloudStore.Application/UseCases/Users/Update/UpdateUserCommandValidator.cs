using FluentValidation;

namespace CloudStore.Application.UseCases.Users.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Value cannot be empty.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name cannot be empty.")
            .Length(1, 100)
            .WithMessage("First name must be between 1 and 100 characters long.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name cannot be empty.")
            .Length(1, 100)
            .WithMessage("Last name must be between 1 and 100 characters long.");
    }
}