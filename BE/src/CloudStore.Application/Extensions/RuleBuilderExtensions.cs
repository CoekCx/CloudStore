using FluentValidation;

namespace CloudStore.Application.Extensions;

public static class RuleBuilderExtensions
{
    private static readonly char[] InvalidFileNameChars = Path.GetInvalidFileNameChars();

    public static IRuleBuilderOptions<T, string> MustHaveValidFileName<T>(this IRuleBuilder<T, string> ruleBuilder) =>
        ruleBuilder
            .Must(name => name.Any(c => !InvalidFileNameChars.Contains(c)));
} 