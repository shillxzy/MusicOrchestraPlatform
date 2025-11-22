namespace MusicOrchestraPlatform.Aggregator.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }

    public class DiscussionDto
    {
        public int Id { get; set; }
        public string Topic { get; set; } = default!;
        public List<CommentDto> Comments { get; set; } = new();
    }

    public class ReviewDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; } = default!;
        public List<DiscussionDto> Discussions { get; set; } = new();
    }
}
