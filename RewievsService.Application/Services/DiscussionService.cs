using RewievsService.Domain.Entities;
using RewievsService.Domain.Interfaces.Repositories;
using RewievsService.Domain.Interfaces.Services;


namespace RewievsService.Application.Services
{
    public class DiscussionService : IDiscussionService
    {
        private readonly IDiscussionRepository _discussionRepository;

        public DiscussionService(IDiscussionRepository discussionRepository)
        {
            _discussionRepository = discussionRepository;
        }

        public async Task<Discussion> CreateAsync(Discussion discussion, CancellationToken cancellationToken = default)
        {
            return await _discussionRepository.AddAsync(discussion, cancellationToken);
        }

        public async Task<Discussion?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _discussionRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<IReadOnlyList<Discussion>> GetByRelatedEntityIdAsync(string relatedEntityId, CancellationToken cancellationToken = default)
        {
            return await _discussionRepository.GetByRelatedEntityIdAsync(relatedEntityId, cancellationToken);
        }

        public async Task<IReadOnlyList<Discussion>> GetByAuthorIdAsync(string authorId, CancellationToken cancellationToken = default)
        {
            return await _discussionRepository.GetByAuthorIdAsync(authorId, cancellationToken);
        }

        public async Task UpdateAsync(Discussion discussion, CancellationToken cancellationToken = default)
        {
            await _discussionRepository.UpdateAsync(discussion, cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            await _discussionRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
