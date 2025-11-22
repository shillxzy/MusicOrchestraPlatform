using MusicOrchestraPlatform.Aggregator.Clients.Interfaces;
using MusicOrchestraPlatform.Aggregator.DTOs;

namespace MusicOrchestraPlatform.Aggregator.Clients
{
    public class ReviewsClient : IReviewsClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ReviewsClient> _logger;

        public ReviewsClient(HttpClient httpClient, ILogger<ReviewsClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ReviewDto?> GetReviewByProductIdAsync(int productId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Calling Reviews service for ProductId {ProductId}", productId);
            var response = await _httpClient.GetAsync($"/api/reviews/{productId}", cancellationToken);
            response.EnsureSuccessStatusCode();
            var review = await response.Content.ReadFromJsonAsync<ReviewDto>(cancellationToken: cancellationToken);
            _logger.LogInformation("Reviews service responded for ProductId {ProductId}", productId);
            return review;
        }
    }
}
