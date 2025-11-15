using MongoDB.Driver;
using ReviewsService.Infrastructure.Repositories;
using RewievsService.Domain.Common;
using RewievsService.Domain.Entities;
using RewievsService.Domain.Interfaces.Repositories;
using RewievsService.Infrastructure.Data;

namespace RewievsService.Infrastructure.Repositories
{
    public class ReviewRepository : MongoRepository<Review>, IReviewRepository
    {
        public ReviewRepository(MongoDbContext context) : base(context) { }

        public async Task<IReadOnlyList<Review>> GetByCompositionIdAsync(string compositionId, CancellationToken cancellationToken = default)
        {
            return await _collection.Find(r => r.TargetId == compositionId)
                                    .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Review>> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await _collection.Find(r => r.CreatedBy == userId)
                                    .ToListAsync(cancellationToken);
        }

        public async Task<double> GetAverageRatingAsync(string compositionId, CancellationToken cancellationToken = default)
        {
            var reviews = await _collection.Find(r => r.TargetId == compositionId)
                                           .ToListAsync(cancellationToken);
            if (reviews.Count == 0) return 0.0;
            return reviews.Average(r => r.Rating.Value);
        }

        public async Task<IReadOnlyList<Review>> GetByTargetIdAsync(string targetId, CancellationToken cancellationToken)
        {
            return await _collection.Find(r => r.TargetId == targetId)
                                    .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Review>> GetByAuthorIdAsync(string authorId, CancellationToken cancellationToken)
        {
            return await _collection.Find(r => r.CreatedBy == authorId)
                                    .ToListAsync(cancellationToken);
        }
    }

}
