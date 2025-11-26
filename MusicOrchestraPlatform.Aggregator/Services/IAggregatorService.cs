using Grpc.Net.Client;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using OrderService.Grpc;
using CatalogService.Grpc;
using ReviewsService.Grpc;

namespace MusicOrchestraPlatform.Aggregator.Services
{
    public interface IAggregatorService
    {
        Task<AggregatedResponse> GetAggregatedDataAsync(CancellationToken cancellationToken = default);
    }

    public class AggregatorService : IAggregatorService
    {
        private readonly OrderGrpc.OrderGrpcClient _ordersClient;
        private readonly CatalogGrpc.CatalogGrpcClient _catalogClient;
        private readonly ReviewsGrpc.ReviewsGrpcClient _reviewsClient;
        private readonly IDistributedCache _redis;
        private readonly ILogger<AggregatorService> _logger;

        private const string CacheKey = "Aggregator:CompositeData";
        private readonly TimeSpan CacheTtl = TimeSpan.FromSeconds(60);

        public AggregatorService(
            OrderGrpc.OrderGrpcClient ordersClient,
            CatalogGrpc.CatalogGrpcClient catalogClient,
            ReviewsGrpc.ReviewsGrpcClient reviewsClient,
            IDistributedCache redis,
            ILogger<AggregatorService> logger)
        {
            _ordersClient = ordersClient;
            _catalogClient = catalogClient;
            _reviewsClient = reviewsClient;
            _redis = redis;
            _logger = logger;
        }

        public async Task<AggregatedResponse> GetAggregatedDataAsync(CancellationToken cancellationToken = default)
        {

            var cached = await _redis.GetStringAsync(CacheKey, cancellationToken);
            if (!string.IsNullOrEmpty(cached))
            {
                _logger.LogInformation("Aggregator cache HIT");
                return System.Text.Json.JsonSerializer.Deserialize<AggregatedResponse>(cached)!;
            }

            _logger.LogInformation("Aggregator cache MISS");

            var catalogTask = SafeGrpcCall<CatalogListResponse>(
     () => _catalogClient.GetAllItemsAsync(new CatalogService.Grpc.Empty(), cancellationToken: cancellationToken).ResponseAsync);

            var ordersTask = SafeGrpcCall<OrderListResponse>(
                () => _ordersClient.GetAllOrdersAsync(new OrderService.Grpc.Empty(), cancellationToken: cancellationToken).ResponseAsync);

            var reviewsTask = SafeGrpcCall<ReviewListResponse>(
                () => _reviewsClient.GetAllReviewsAsync(new ReviewsService.Grpc.Empty(), cancellationToken: cancellationToken).ResponseAsync);


            await Task.WhenAll(catalogTask, ordersTask, reviewsTask);

            var aggregated = new AggregatedResponse
            {
                CatalogItems = catalogTask.Result?.Items.ToList() ?? new(),
                Orders = ordersTask.Result?.Orders.ToList() ?? new(),
                Reviews = reviewsTask.Result?.Reviews.ToList() ?? new()
            };

            var serialized = System.Text.Json.JsonSerializer.Serialize(aggregated);
            await _redis.SetStringAsync(CacheKey, serialized, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = CacheTtl
            }, cancellationToken);

            return aggregated;
        }

        private async Task<T?> SafeGrpcCall<T>(Func<Task<T>> call)
        {
            try
            {
                return await call();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "gRPC call failed");
                return default;
            }
        }
    }

    public class AggregatedResponse
    {
        public List<CatalogResponse> CatalogItems { get; set; } = new();
        public List<OrderResponse> Orders { get; set; } = new();
        public List<ReviewResponse> Reviews { get; set; } = new();
    }
}
