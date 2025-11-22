using MusicOrchestraPlatform.Aggregator.DTOs;

namespace MusicOrchestraPlatform.Aggregator.Clients.Interfaces
{
    public interface IReviewsClient
    {
        Task<ReviewDto?> GetReviewByProductIdAsync(int productId, CancellationToken cancellationToken = default);
    }
}
