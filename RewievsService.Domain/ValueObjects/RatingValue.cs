using RewievsService.Domain.Common;
using ReviewsService.Domain.Exceptions;

namespace RewievsService.Domain.ValueObjects
{
    public class RatingValue : ValueObject
    {
        public int Value { get; private set; }

        private RatingValue() { }

        public RatingValue(int value)
        {
            if (value < 1 || value > 5)
                throw new DomainException("Rating must be between 1 and 5");

            Value = value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
