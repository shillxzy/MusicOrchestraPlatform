using RewievsService.Domain.Entities;
using RewievsService.Domain.Interfaces.Repositories;
using RewievsService.Domain.Interfaces.Services;


namespace RewievsService.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Review> CreateAsync(Review review, CancellationToken cancellationToken = default)
        {
            return await _reviewRepository.AddAsync(review, cancellationToken);
        }

        public async Task<Review?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            return await _reviewRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<Review?> GetByIdWithCommentsAsync(string id, CancellationToken cancellationToken = default)
        {
            var review = await _reviewRepository.GetByIdAsync(id, cancellationToken);
            if (review == null)
                return null;

            // Коментарі вже всередині Review.Comments
            return review;
        }

        public async Task<IReadOnlyList<Review>> GetByTargetIdAsync(string targetId, CancellationToken cancellationToken = default)
        {
            return await _reviewRepository.GetByTargetIdAsync(targetId, cancellationToken);
        }

        public async Task<IReadOnlyList<Review>> GetByAuthorIdAsync(string authorId, CancellationToken cancellationToken = default)
        {
            return await _reviewRepository.GetByAuthorIdAsync(authorId, cancellationToken);
        }

        public async Task UpdateAsync(Review review, CancellationToken cancellationToken = default)
        {
            await _reviewRepository.UpdateAsync(review, cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            await _reviewRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
