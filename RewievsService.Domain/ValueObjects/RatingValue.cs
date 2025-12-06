using RewievsService.Domain.Common;
using ReviewsService.Domain.Exceptions;
using MongoDB.Bson.Serialization.Attributes;

namespace RewievsService.Domain.ValueObjects
{
    [BsonIgnoreExtraElements]
    public class RatingValue : ValueObject
    {
        [BsonElement("value")]
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
