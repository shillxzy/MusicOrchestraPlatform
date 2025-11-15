using MongoDB.Driver;
using ReviewsService.Infrastructure.Repositories;
using RewievsService.Domain.Entities;
using RewievsService.Domain.Interfaces.Repositories;
using RewievsService.Infrastructure.Data;

namespace RewievsService.Infrastructure.Repositories
{
    public class CommentRepository : MongoRepository<Comment>, ICommentRepository
    {
        public CommentRepository(MongoDbContext context) : base(context) { }

        public async Task<IReadOnlyList<Comment>> GetByAuthorIdAsync(string authorId, CancellationToken cancellationToken)
        {
            return await _collection.Find(c => c.CreatedBy == authorId)
                                    .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Comment>> GetByReviewIdAsync(string reviewId, CancellationToken cancellationToken = default)
        {
            return await _collection.Find(c => c.ParentCommentId == reviewId)
                                    .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Comment>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _collection.Find(c => c.CreatedBy == userId)
                                    .ToListAsync(cancellationToken);
        }
    }
}
