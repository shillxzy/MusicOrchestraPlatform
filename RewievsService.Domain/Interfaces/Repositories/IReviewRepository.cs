using RewievsService.Domain.Common;
using RewievsService.Domain.Entities;

namespace RewievsService.Domain.Interfaces.Repositories
{
    public interface IReviewRepository : IMongoRepository<Review>
    {
        Task<IReadOnlyList<Review>> GetByCompositionIdAsync(string compositionId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Review>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
        Task<double> GetAverageRatingAsync(string compositionId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Review>> GetByTargetIdAsync(string targetId, CancellationToken cancellationToken);
        Task<IReadOnlyList<Review>> GetByAuthorIdAsync(string authorId, CancellationToken cancellationToken);
    }
}