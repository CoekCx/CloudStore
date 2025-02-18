using FluentValidation;

namespace CloudStore.Application.Features.Directories.GetContents;

public class GetDirectoryContentsQueryValidator : AbstractValidator<GetDirectoryContentsQuery>
{
    public GetDirectoryContentsQueryValidator()
    {
        RuleFor(x => x.DirectoryId)
            .NotEmpty().WithMessage("Directory ID is required.");

        RuleFor(x => x.OwnerId)
            .NotEmpty().WithMessage("Owner ID is required.");
    }
} 