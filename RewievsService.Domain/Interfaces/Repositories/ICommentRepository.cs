using RewievsService.Domain.Common;
using RewievsService.Domain.Entities;

namespace RewievsService.Domain.Interfaces.Repositories
{
    public interface ICommentRepository : IMongoRepository<Comment>
    {
        Task<IReadOnlyList<Comment>> GetByAuthorIdAsync(string authorId, CancellationToken cancellationToken);
        Task<IReadOnlyList<Comment>> GetByReviewIdAsync(string reviewId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Comment>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    }
}
