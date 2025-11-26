using Grpc.Core;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using OrderService.Grpc;
using System.Text.Json;

namespace OrderService.API.GrpcServices
{
    public class OrderGrpcService : OrderGrpc.OrderGrpcBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _redisCache;
        private readonly ILogger<OrderGrpcService> _logger;

        public OrderGrpcService(IMemoryCache memoryCache, IDistributedCache redisCache, ILogger<OrderGrpcService> logger)
        {
            _memoryCache = memoryCache;
            _redisCache = redisCache;
            _logger = logger;
        }

        public override async Task<OrderResponse> GetOrder(OrderRequest request, ServerCallContext context)
        {
            string key = $"Order_{request.Id}";

            if (!_memoryCache.TryGetValue(key, out OrderResponse order))
            {
                _logger.LogInformation("L1 Cache MISS for {Key}", key);

                var redisData = await _redisCache.GetStringAsync(key);
                if (!string.IsNullOrEmpty(redisData))
                {
                    order = JsonSerializer.Deserialize<OrderResponse>(redisData);
                    _logger.LogInformation("L2 Cache HIT for {Key}", key);

                    _memoryCache.Set(key, order, new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                        .SetSize(1));
                }
                else
                {
                    _logger.LogInformation("L2 Cache MISS for {Key}", key);

                    order = new OrderResponse
                    {
                        Id = request.Id,
                        CatalogItemId = request.Id * 10,
                        Quantity = 1 + request.Id % 5,
                        Status = "Pending"
                    };

                    var serialized = JsonSerializer.Serialize(order);
                    await _redisCache.SetStringAsync(key, serialized, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                    });

                    _memoryCache.Set(key, order, new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                        .SetSize(1));
                }
            }
            else
            {
                _logger.LogInformation("L1 Cache HIT for {Key}", key);
            }

            return order;
        }

        public async Task<OrderResponse> AddOrUpdateOrder(OrderResponse order)
        {
            string key = $"Order_{order.Id}";
            _memoryCache.Remove(key);
            await _redisCache.RemoveAsync(key);
            _logger.LogInformation("Cache invalidated: {Key}", key);
            return order;
        }

        public async Task DeleteOrder(int id)
        {
            string key = $"Order_{id}";
            _memoryCache.Remove(key);
            await _redisCache.RemoveAsync(key);
            _logger.LogInformation("Cache invalidated: {Key}", key);
        }
    }


}
