using Grpc.Core;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using ReviewsService.Grpc;
using System.Text.Json;

namespace RewievsService.API.GrpcServices
{
    public class ReviewsGrpcService : ReviewsGrpc.ReviewsGrpcBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _redisCache;
        private readonly ILogger<ReviewsGrpcService> _logger;

        public ReviewsGrpcService(IMemoryCache memoryCache, IDistributedCache redisCache, ILogger<ReviewsGrpcService> logger)
        {
            _memoryCache = memoryCache;
            _redisCache = redisCache;
            _logger = logger;
        }

        public override async Task<ReviewResponse> GetReview(ReviewRequest request, ServerCallContext context)
        {
            string key = $"Review_{request.Id}";

            if (!_memoryCache.TryGetValue(key, out ReviewResponse review))
            {
                _logger.LogInformation("L1 Cache MISS for {Key}", key);

                var redisData = await _redisCache.GetStringAsync(key);
                if (!string.IsNullOrEmpty(redisData))
                {
                    review = JsonSerializer.Deserialize<ReviewResponse>(redisData);
                    _logger.LogInformation("L2 Cache HIT for {Key}", key);

                    _memoryCache.Set(key, review, new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                        .SetSize(1));
                }
                else
                {
                    _logger.LogInformation("L2 Cache MISS for {Key}", key);

                    review = new ReviewResponse
                    {
                        Id = request.Id,
                        CatalogItemId = request.Id * 10,
                        Author = $"Author {request.Id}",
                        Content = $"Review content {request.Id}",
                        Rating = (request.Id % 5) + 1
                    };

                    var serialized = JsonSerializer.Serialize(review);
                    await _redisCache.SetStringAsync(key, serialized, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                    });

                    _memoryCache.Set(key, review, new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                        .SetSize(1));
                }
            }
            else
            {
                _logger.LogInformation("L1 Cache HIT for {Key}", key);
            }

            return review;
        }

        public async Task<ReviewResponse> AddOrUpdateReview(ReviewResponse review)
        {
            string key = $"Review_{review.Id}";
            _memoryCache.Remove(key);
            await _redisCache.RemoveAsync(key);
            _logger.LogInformation("Cache invalidated: {Key}", key);
            return review;
        }

        public async Task DeleteReview(int id)
        {
            string key = $"Review_{id}";
            _memoryCache.Remove(key);
            await _redisCache.RemoveAsync(key);
            _logger.LogInformation("Cache invalidated: {Key}", key);
        }
    }


}
