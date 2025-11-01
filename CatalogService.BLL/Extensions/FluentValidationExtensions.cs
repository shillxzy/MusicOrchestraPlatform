using FluentValidation;

namespace CatalogService.BLL.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> RequiredWithMaxLength<T>(
            this IRuleBuilder<T, string> ruleBuilder, int maxLength, string fieldName)
        {
            return ruleBuilder
                .NotEmpty().WithMessage($"{fieldName} is required.")
                .MaximumLength(maxLength).WithMessage($"{fieldName} cannot exceed {maxLength} characters.");
        }

        public static IRuleBuilderOptions<T, int> MustBePositive<T>(
            this IRuleBuilder<T, int> ruleBuilder, string fieldName)
        {
            return ruleBuilder
                .GreaterThan(0).WithMessage($"{fieldName} must be greater than zero.");
        }
        public static IRuleBuilderOptions<T, decimal> MustBePositive<T>(
            this IRuleBuilder<T, decimal> ruleBuilder, string fieldName)
        {
            return ruleBuilder
                .GreaterThan(0).WithMessage($"{fieldName} must be greater than zero.");
        }

        public static IRuleBuilderOptions<T, TimeSpan> MustBePositive<T>(
            this IRuleBuilder<T, TimeSpan> ruleBuilder, string fieldName)
        {
            return ruleBuilder
                .GreaterThan(TimeSpan.Zero).WithMessage($"{fieldName} must be greater than zero.");
        }
    }
}
