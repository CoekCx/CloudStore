using FluentValidation;

namespace CloudStore.Application.UseCases.Directories.Delete;

public class DeleteDirectoryCommandValidator : AbstractValidator<DeleteDirectoryCommand>
{
    public DeleteDirectoryCommandValidator()
    {
        RuleFor(x => x.DirectoryId)
            .NotEmpty().WithMessage("Directory ID is required.");

        RuleFor(x => x.OwnerId)
            .NotEmpty().WithMessage("Owner ID is required.");
    }
} 