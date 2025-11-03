using MongoDB.Bson.Serialization.Attributes;
using RewievsService.Domain.Common;
using ReviewsService.Domain.Exceptions;
using RewievsService.Domain.ValueObjects;

namespace RewievsService.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Review : BaseEntity
    {
        [BsonElement("title")]
        public string Title { get; private set; }

        [BsonElement("content")]
        public string Content { get; private set; }

        [BsonElement("rating")]
        public RatingValue Rating { get; private set; }

        [BsonElement("targetId")]
        public string TargetId { get; private set; } // Наприклад, ID продукту, інструменту чи програми

        [BsonElement("comments")]
        public List<Comment> Comments { get; private set; } = new();

        private Review() { }

        public Review(string title, string content, RatingValue rating, string targetId, string? createdBy = null)
            : base(createdBy)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Title cannot be empty");
            if (string.IsNullOrWhiteSpace(content))
                throw new DomainException("Content cannot be empty");

            Title = title;
            Content = content;
            Rating = rating ?? throw new DomainException("Rating is required");
            TargetId = targetId ?? throw new DomainException("TargetId is required");
        }

        public void Update(string title, string content, RatingValue rating, string updatedBy)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Title cannot be empty");
            if (string.IsNullOrWhiteSpace(content))
                throw new DomainException("Content cannot be empty");

            Title = title;
            Content = content;
            Rating = rating;
            SetUpdated(updatedBy);
        }

        public void AddComment(Comment comment)
        {
            if (comment == null)
                throw new DomainException("Comment cannot be null");

            Comments.Add(comment);
        }

        public void RemoveComment(string commentId)
        {
            var comment = Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment == null)
                throw new NotFoundException($"Comment with id {commentId} not found");

            Comments.Remove(comment);
        }
    }
}
