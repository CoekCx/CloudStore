using FluentValidation;

namespace CloudStore.Application.UseCases.Directories.Move;

public class MoveDirectoryCommandValidator : AbstractValidator<MoveDirectoryCommand>
{
    public MoveDirectoryCommandValidator()
    {
        RuleFor(x => x.DirectoryId)
            .NotEmpty().WithMessage("Directory ID is required.");

        RuleFor(x => x.OwnerId)
            .NotEmpty().WithMessage("Owner ID is required.");
    }
} 