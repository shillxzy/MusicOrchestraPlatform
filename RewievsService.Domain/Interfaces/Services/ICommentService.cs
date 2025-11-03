using RewievsService.Domain.Entities;

namespace RewievsService.Domain.Interfaces.Services
{
    public interface ICommentService
    {
        Task<Comment> CreateAsync(Comment comment, CancellationToken cancellationToken = default);
        Task<Comment?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Comment>> GetByReviewIdAsync(string reviewId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Comment>> GetByAuthorIdAsync(string authorId, CancellationToken cancellationToken = default);
        Task UpdateAsync(Comment comment, CancellationToken cancellationToken = default);
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}
