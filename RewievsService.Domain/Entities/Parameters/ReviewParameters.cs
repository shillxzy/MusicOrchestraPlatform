namespace RewievsService.Domain.Entities.Parameters
{
    public class ReviewParameters : QueryStringParameters
    {
        public string? UserId { get; set; }
        public string? TargetId { get; set; }
        public int? MinRating { get; set; }
        public int? MaxRating { get; set; } = 10;
    }
}
