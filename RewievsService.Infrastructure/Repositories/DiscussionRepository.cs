using MongoDB.Driver;
using ReviewsService.Infrastructure.Repositories;
using RewievsService.Domain.Entities;
using RewievsService.Domain.Interfaces.Repositories;
using RewievsService.Infrastructure.Data;


namespace RewievsService.Infrastructure.Repositories
{
    public class DiscussionRepository : MongoRepository<Discussion>, IDiscussionRepository
    {
        public DiscussionRepository(MongoDbContext context) : base(context) { }

        public async Task<IReadOnlyList<Discussion>> GetByRelatedEntityIdAsync(string relatedEntityId, CancellationToken cancellationToken = default)
        {
            return await _collection.Find(d => d.RelatedEntityId == relatedEntityId)
                                    .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Discussion>> GetByAuthorIdAsync(string authorId, CancellationToken cancellationToken = default)
        {
            return await _collection.Find(d => d.CreatedBy == authorId)
                                    .ToListAsync(cancellationToken);
        }
    }
}
