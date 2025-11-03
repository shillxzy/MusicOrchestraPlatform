using RewievsService.Domain.Entities;

namespace RewievsService.Domain.Interfaces.Services
{
    public interface IDiscussionService
    {
        Task<Discussion> CreateAsync(Discussion discussion, CancellationToken cancellationToken = default);
        Task<Discussion?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Discussion>> GetByRelatedEntityIdAsync(string relatedEntityId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Discussion>> GetByAuthorIdAsync(string authorId, CancellationToken cancellationToken = default);
        Task UpdateAsync(Discussion discussion, CancellationToken cancellationToken = default);
        Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    }
}
