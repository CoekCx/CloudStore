using FluentValidation;

namespace CloudStore.Application.Features.Directories.GetById;

public class GetDirectoryByIdQueryValidator : AbstractValidator<GetDirectoryByIdQuery>
{
    public GetDirectoryByIdQueryValidator()
    {
        RuleFor(x => x.DirectoryId)
            .NotEmpty().WithMessage("Directory ID is required.");

        RuleFor(x => x.OwnerId)
            .NotEmpty().WithMessage("Owner ID is required.");
    }
} 