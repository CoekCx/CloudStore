using CloudStore.Application.Helpers;
using FluentValidation;

namespace CloudStore.Application.Features.Directories.Update;

public class UpdateDirectoryCommandValidator : AbstractValidator<UpdateDirectoryCommand>
{
    public UpdateDirectoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Directory ID is required.");

        RuleFor(x => x.OwnerId)
            .NotEmpty().WithMessage("Owner ID is required.");

        RuleFor(x => x.NewName)
            .NotEmpty().WithMessage("Directory name is required.")
            .NotNull().WithMessage("Directory name is null.")
            .MaximumLength(255).WithMessage("Directory name length exceeded.")
            .Must(name => !DirectoryValidatorHelper.ContainsInvalidCharacters(name))
            .WithMessage("Directory name contains invalid characters.");
    }
} 