using Grpc.Core;
using Microsoft.Extensions.Caching.Memory;
using ReviewsService.Grpc;

namespace RewievsService.API.GrpcServices
{
    public class ReviewsGrpcService : ReviewsGrpc.ReviewsGrpcBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<ReviewsGrpcService> _logger;

        public ReviewsGrpcService(IMemoryCache memoryCache, ILogger<ReviewsGrpcService> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public override Task<ReviewResponse> GetReview(ReviewRequest request, ServerCallContext context)
        {
            string key = $"Review_{request.Id}";

            if (!_memoryCache.TryGetValue(key, out ReviewResponse cached))
            {
                _logger.LogInformation("Cache MISS for {Key}", key);

                cached = new ReviewResponse
                {
                    Id = request.Id,
                    CatalogItemId = request.Id * 10,
                    Author = $"Author {request.Id}",
                    Content = $"Review content for item {request.Id}",
                    Rating = (request.Id % 5) + 1
                };

                var options = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .SetSize(1);

                _memoryCache.Set(key, cached, options);
            }
            else
            {
                _logger.LogInformation("Cache HIT for {Key}", key);
            }

            return Task.FromResult(cached);
        }

        // Write operation
        public Task<ReviewResponse> AddOrUpdateReview(ReviewResponse review)
        {
            _logger.LogInformation("Write operation for Review_{Id}", review.Id);

            string key = $"Review_{review.Id}";
            _memoryCache.Remove(key);

            _logger.LogInformation("Cache invalidated: {Key}", key);

            return Task.FromResult(review);
        }

        public Task DeleteReview(int id)
        {
            _logger.LogInformation("Delete operation for Review_{Id}", id);

            string key = $"Review_{id}";
            _memoryCache.Remove(key);

            _logger.LogInformation("Cache invalidated: {Key}", key);

            return Task.CompletedTask;
        }
    }



}
