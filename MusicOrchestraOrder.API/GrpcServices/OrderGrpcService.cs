using Grpc.Core;
using Microsoft.Extensions.Caching.Memory;
using OrderService.Grpc;

namespace OrderService.API.GrpcServices
{
    public class OrderGrpcService : OrderGrpc.OrderGrpcBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<OrderGrpcService> _logger;

        public OrderGrpcService(IMemoryCache memoryCache, ILogger<OrderGrpcService> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public override Task<OrderResponse> GetOrder(OrderRequest request, ServerCallContext context)
        {
            string key = $"Order_{request.Id}";

            if (!_memoryCache.TryGetValue(key, out OrderResponse cached))
            {
                _logger.LogInformation("Cache MISS for {Key}", key);

                cached = new OrderResponse
                {
                    Id = request.Id,
                    CatalogItemId = request.Id * 10,
                    Quantity = 1 + request.Id % 5,
                    Status = "Pending"
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


        public Task<OrderResponse> AddOrUpdateOrder(OrderResponse order)
        {
            _logger.LogInformation("Write operation for Order_{Id}", order.Id);

            string key = $"Order_{order.Id}";
            _memoryCache.Remove(key);

            _logger.LogInformation("Cache invalidated: {Key}", key);

            return Task.FromResult(order);
        }

        public Task DeleteOrder(int id)
        {
            _logger.LogInformation("Delete operation for Order_{Id}", id);

            string key = $"Order_{id}";
            _memoryCache.Remove(key);

            _logger.LogInformation("Cache invalidated: {Key}", key);

            return Task.CompletedTask;
        }
    }



}
