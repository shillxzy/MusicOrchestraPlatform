using MusicOrchestraPlatform.Aggregator.Clients.Interfaces;
using MusicOrchestraPlatform.Aggregator.DTOs;


namespace MusicOrchestraPlatform.Aggregator.Clients
{
    public class CatalogClient : ICatalogClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CatalogClient> _logger;

        public CatalogClient(HttpClient httpClient, ILogger<CatalogClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ProductDto?> GetProductByIdAsync(int productId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Calling Catalog service for ProductId {ProductId}", productId);
            var response = await _httpClient.GetAsync($"/api/catalog/products/{productId}", cancellationToken);
            response.EnsureSuccessStatusCode();
            var product = await response.Content.ReadFromJsonAsync<ProductDto>(cancellationToken: cancellationToken);
            _logger.LogInformation("Catalog service responded for ProductId {ProductId}", productId);
            return product;
        }
    }
}
