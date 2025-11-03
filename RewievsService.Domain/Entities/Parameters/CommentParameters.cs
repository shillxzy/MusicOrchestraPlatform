namespace RewievsService.Domain.Entities.Parameters
{
    public class CommentParameters : QueryStringParameters
    {
        public string? ReviewId { get; set; }
        public string? AuthorId { get; set; }
    }
}
