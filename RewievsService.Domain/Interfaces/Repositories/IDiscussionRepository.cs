using RewievsService.Domain.Common;
using RewievsService.Domain.Entities;

namespace RewievsService.Domain.Interfaces.Repositories
{
    public interface IDiscussionRepository : IMongoRepository<Discussion>
    {
        Task<IReadOnlyList<Discussion>> GetByRelatedEntityIdAsync(string relatedEntityId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Discussion>> GetByAuthorIdAsync(string authorId, CancellationToken cancellationToken = default);
    }
}
