using FluentValidation;

namespace CloudStore.Application.UseCases.Directories.GetRoot;

public class GetRootDirectoryQueryValidator : AbstractValidator<GetRootDirectoryQuery>
{
    public GetRootDirectoryQueryValidator()
    {
        RuleFor(x => x.OwnerId)
            .NotEmpty().WithMessage("Owner ID is required.");
    }
} 