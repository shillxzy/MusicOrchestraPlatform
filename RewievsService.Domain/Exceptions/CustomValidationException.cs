using FluentValidation;
using FluentValidation.Results;

namespace ReviewsService.Domain.Exceptions
{
    public class CustomValidationException : ValidationException
    {
        public CustomValidationException(IEnumerable<ValidationFailure> failures)
            : base(failures) { }
    }
}