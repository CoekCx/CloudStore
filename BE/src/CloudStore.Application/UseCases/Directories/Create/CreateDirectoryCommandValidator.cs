using CloudStore.Application.Extensions;
using FluentValidation;

namespace CloudStore.Application.UseCases.Directories.Create;

public class CreateDirectoryCommandValidator : AbstractValidator<CreateDirectoryCommand>
{
    public CreateDirectoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Directory name is required.")
            .NotNull().WithMessage("Directory name is null.")
            .MaximumLength(255).WithMessage("Directory name length exceeded.")
            .MustHaveValidFileName()
            .WithMessage("Directory name contains invalid characters.");

        RuleFor(x => x.OwnerId)
            .NotEmpty().WithMessage("Owner ID is required.");
    }
}