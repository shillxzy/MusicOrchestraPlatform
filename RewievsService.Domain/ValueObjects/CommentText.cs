using RewievsService.Domain.Common;
using ReviewsService.Domain.Exceptions;

namespace RewievsService.Domain.ValueObjects
{
    public class CommentText : ValueObject
    {
        public string Value { get; private set; }

        private CommentText() { }

        public CommentText(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Comment cannot be empty");

            if (value.Length > 500)
                throw new DomainException("Comment cannot exceed 500 characters");

            Value = value.Trim();
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
