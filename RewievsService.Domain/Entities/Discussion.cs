using MongoDB.Bson.Serialization.Attributes;
using RewievsService.Domain.Common;
using ReviewsService.Domain.Exceptions;

namespace RewievsService.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Discussion : BaseEntity
    {
        [BsonElement("topic")]
        public string Topic { get; private set; }

        [BsonElement("relatedEntityId")]
        public string RelatedEntityId { get; private set; }

        [BsonElement("reviews")]
        public List<Review> Reviews { get; private set; } = new();

        private Discussion() { }

        public Discussion(string topic, string relatedEntityId, string? createdBy = null)
            : base(createdBy)
        {
            if (string.IsNullOrWhiteSpace(topic))
                throw new DomainException("Topic cannot be empty");

            if (string.IsNullOrWhiteSpace(relatedEntityId))
                throw new DomainException("RelatedEntityId cannot be empty");

            Topic = topic;
            RelatedEntityId = relatedEntityId;
        }

        public void AddReview(Review review)
        {
            if (review == null)
                throw new DomainException("Review cannot be null");

            Reviews.Add(review);
        }

        public void RemoveReview(string reviewId)
        {
            var review = Reviews.FirstOrDefault(r => r.Id == reviewId);
            if (review == null)
                throw new NotFoundException($"Review with id {reviewId} not found");

            Reviews.Remove(review);
        }
    }
}
