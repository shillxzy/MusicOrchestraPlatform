using RewievsService.Domain.Entities;
using RewievsService.Domain.Interfaces.Repositories;
using RewievsService.Domain.Interfaces.Services;


namespace RewievsService.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Comment> CreateAsync(Comment comment, CancellationToken cancellationToken = default)
        {
            return await _commentRepository.AddAsync(comment, cancellationToken);
        }

        public async Task<Comment?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _commentRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<IReadOnlyList<Comment>> GetByReviewIdAsync(string reviewId, CancellationToken cancellationToken = default)
        {
            return await _commentRepository.GetByReviewIdAsync(reviewId, cancellationToken);
        }

        public async Task<IReadOnlyList<Comment>> GetByAuthorIdAsync(string authorId, CancellationToken cancellationToken = default)
        {
            return await _commentRepository.GetByAuthorIdAsync(authorId, cancellationToken);
        }

        public async Task UpdateAsync(Comment comment, CancellationToken cancellationToken = default)
        {
            await _commentRepository.UpdateAsync(comment, cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            await _commentRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
