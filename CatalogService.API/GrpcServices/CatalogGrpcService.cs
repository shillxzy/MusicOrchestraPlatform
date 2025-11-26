using CatalogService.Grpc;
using Grpc.Core;
using Microsoft.Extensions.Caching.Memory;

namespace CatalogService.API.GrpcServices
{
    public class CatalogGrpcService : CatalogGrpc.CatalogGrpcBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CatalogGrpcService> _logger;

        public CatalogGrpcService(IMemoryCache memoryCache, ILogger<CatalogGrpcService> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public override Task<CatalogResponse> GetItem(CatalogRequest request, ServerCallContext context)
        {
            string cacheKey = $"CatalogItem_{request.Id}";

            if (!_memoryCache.TryGetValue(cacheKey, out CatalogResponse cachedItem))
            {
                _logger.LogInformation("Cache MISS for {CacheKey}", cacheKey);

                cachedItem = new CatalogResponse
                {
                    Id = request.Id,
                    Name = $"Item {request.Id}",
                    Description = $"Description for item {request.Id}",
                    Price = 99.9 + request.Id
                };

                var options = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                    .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                    .SetSize(1);

                _memoryCache.Set(cacheKey, cachedItem, options);
            }
            else
            {
                _logger.LogInformation("Cache HIT for {CacheKey}", cacheKey);
            }

            return Task.FromResult(cachedItem);
        }


        public Task<CatalogResponse> AddOrUpdateItem(CatalogResponse item)
        {
       
            _logger.LogInformation("Write operation for CatalogItem_{Id}", item.Id);

     
            string itemKey = $"CatalogItem_{item.Id}";
            string allItemsKey = "Catalog_AllItems";

            _memoryCache.Remove(itemKey);
            _memoryCache.Remove(allItemsKey);

            _logger.LogInformation("Cache invalidated: {Keys}", new[] { itemKey, allItemsKey });

            return Task.FromResult(item);
        }

       
        public Task DeleteItem(int id)
        {
       
            _logger.LogInformation("Delete operation for CatalogItem_{Id}", id);

            string itemKey = $"CatalogItem_{id}";
            string allItemsKey = "Catalog_AllItems";

            _memoryCache.Remove(itemKey);
            _memoryCache.Remove(allItemsKey);

            _logger.LogInformation("Cache invalidated: {Keys}", new[] { itemKey, allItemsKey });

            return Task.CompletedTask;
        }
    }




}
