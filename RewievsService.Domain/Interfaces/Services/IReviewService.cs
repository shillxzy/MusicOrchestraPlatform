using RewievsService.Domain.Entities;

namespace RewievsService.Domain.Interfaces.Services
{
    public interface IReviewService
    {
        Task<Review> CreateAsync(Review review, CancellationToken cancellationToken = default);
        Task<Review?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<Review?> GetByIdWithCommentsAsync(string id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Review>> GetByTargetIdAsync(string targetId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Review>> GetByAuthorIdAsync(string authorId, CancellationToken cancellationToken = default);
        Task UpdateAsync(Review review, CancellationToken cancellationToken = default);
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}
