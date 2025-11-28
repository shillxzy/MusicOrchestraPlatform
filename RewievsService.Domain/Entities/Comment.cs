using MongoDB.Bson.Serialization.Attributes;
using RewievsService.Domain.Common;
using ReviewsService.Domain.Exceptions;
using RewievsService.Domain.ValueObjects;

namespace RewievsService.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Comment : BaseEntity
    {
        [BsonElement("authorId")]
        public string AuthorId { get; private set; }

        [BsonElement("text")]
        public CommentText Text { get; private set; }

        [BsonElement("likes")]
        public int Likes { get; private set; }

        [BsonElement("parentCommentId")]
        public string? ParentCommentId { get; private set; }

        [BsonIgnore]
        public List<Comment>? Replies { get; set; }


        private Comment() { }

        public Comment(string authorId, CommentText text, string? parentCommentId = null, string? createdBy = null)
            : base(createdBy)
        {
            if (string.IsNullOrWhiteSpace(authorId))
                throw new DomainException("AuthorId cannot be empty");

            AuthorId = authorId;
            Text = text ?? throw new DomainException("Text cannot be null");
            ParentCommentId = parentCommentId;
            Likes = 0;
        }

        public void UpdateText(CommentText newText, string updatedBy)
        {
            if (newText == null)
                throw new DomainException("Text cannot be null");

            Text = newText;
            SetUpdated(updatedBy);
        }

        public void AddLike() => Likes++;
        public void RemoveLike()
        {
            if (Likes > 0)
                Likes--;
        }
    }
}
