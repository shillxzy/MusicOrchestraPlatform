using RewievsService.Domain.Common;
using ReviewsService.Domain.Exceptions;
using MongoDB.Bson.Serialization.Attributes;


namespace RewievsService.Domain.ValueObjects
{

    [BsonIgnoreExtraElements]
    public class CommentText : ValueObject
    {
        [BsonElement("value")]
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
