using CatalogService.Grpc;
using Grpc.Core;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace CatalogService.API.GrpcServices
{
    public class CatalogGrpcService : CatalogGrpc.CatalogGrpcBase
    {
        private readonly IMemoryCache _memoryCache;                
        private readonly IDistributedCache _redisCache;             
        private readonly ILogger<CatalogGrpcService> _logger;

        public CatalogGrpcService(IMemoryCache memoryCache, IDistributedCache redisCache, ILogger<CatalogGrpcService> logger)
        {
            _memoryCache = memoryCache;
            _redisCache = redisCache;
            _logger = logger;
        }

        // Read operation with two-level cache
        public override async Task<CatalogResponse> GetItem(CatalogRequest request, ServerCallContext context)
        {
            string cacheKey = $"CatalogItem_{request.Id}";
            if (!_memoryCache.TryGetValue(cacheKey, out CatalogResponse item))
            {
                _logger.LogInformation("L1 Cache MISS for {CacheKey}", cacheKey);

                // Шукаємо в Redis (L2)
                var redisData = await _redisCache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(redisData))
                {
                    item = JsonSerializer.Deserialize<CatalogResponse>(redisData);
                    _logger.LogInformation("L2 Cache HIT for {CacheKey}", cacheKey);

                    // Записуємо в L1
                    _memoryCache.Set(cacheKey, item, new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                        .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                        .SetSize(1));
                }
                else
                {
                    _logger.LogInformation("L2 Cache MISS for {CacheKey}", cacheKey);

                    // Імітуємо DB
                    item = new CatalogResponse
                    {
                        Id = request.Id,
                        Name = $"Item {request.Id}",
                        Description = $"Description for item {request.Id}",
                        Price = 99.9 + request.Id
                    };

                    var serialized = JsonSerializer.Serialize(item);
                    await _redisCache.SetStringAsync(cacheKey, serialized, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // TTL L2
                    });

                    _memoryCache.Set(cacheKey, item, new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                        .SetSize(1));
                }
            }
            else
            {
                _logger.LogInformation("L1 Cache HIT for {CacheKey}", cacheKey);
            }

            return item;
        }

        // Write operation — Add/Update
        public async Task<CatalogResponse> AddOrUpdateItem(CatalogResponse item)
        {
            _logger.LogInformation("Write operation for CatalogItem_{Id}", item.Id);

            string cacheKey = $"CatalogItem_{item.Id}";
            _memoryCache.Remove(cacheKey);
            await _redisCache.RemoveAsync(cacheKey);
            _logger.LogInformation("Cache invalidated: {CacheKey}", cacheKey);

            return item;
        }

        // Delete operation
        public async Task DeleteItem(int id)
        {
            _logger.LogInformation("Delete operation for CatalogItem_{Id}", id);

            string cacheKey = $"CatalogItem_{id}";
            _memoryCache.Remove(cacheKey);
            await _redisCache.RemoveAsync(cacheKey);
            _logger.LogInformation("Cache invalidated: {CacheKey}", cacheKey);
        }

        // Optional: cache warming
        public async Task WarmupCache(IEnumerable<int> ids)
        {
            foreach (var id in ids)
            {
                var key = $"CatalogItem_{id}";
                var item = new CatalogResponse
                {
                    Id = id,
                    Name = $"Item {id}",
                    Description = $"Description {id}",
                    Price = 99.9 + id
                };
                var serialized = JsonSerializer.Serialize(item);
                await _redisCache.SetStringAsync(key, serialized, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
                _memoryCache.Set(key, item, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                    .SetSize(1));
            }

            _logger.LogInformation("Cache warming completed for {Count} items", ids.Count());
        }
    }


}
