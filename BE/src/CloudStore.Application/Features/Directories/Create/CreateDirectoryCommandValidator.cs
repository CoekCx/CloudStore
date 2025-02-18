using CloudStore.Application.Helpers;
using FluentValidation;

namespace CloudStore.Application.Features.Directories.Create;

public class CreateDirectoryCommandValidator : AbstractValidator<CreateDirectoryCommand>
{
    public CreateDirectoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Directory name is required.")
            .NotNull().WithMessage("Directory name is null.")
            .MaximumLength(255).WithMessage("Directory name length exceeded.")
            .Must(name => !DirectoryValidatorHelper.ContainsInvalidCharacters(name))
            .WithMessage("Directory name contains invalid characters.");

        RuleFor(x => x.OwnerId)
            .NotEmpty().WithMessage("Owner ID is required.");
    }
}