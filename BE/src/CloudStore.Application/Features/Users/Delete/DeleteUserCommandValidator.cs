using FluentValidation;

namespace CloudStore.Application.Features.Users.Delete;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("UserId cannot be empty.");
    }
}