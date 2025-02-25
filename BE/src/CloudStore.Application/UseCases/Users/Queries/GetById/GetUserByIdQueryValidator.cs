using CloudStore.Application.UseCases.Directories.Queries.GetById;
using FluentValidation;

namespace CloudStore.Application.UseCases.Users.Queries.GetById;

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID is required.");
    }
} 