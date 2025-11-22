using MusicOrchestraPlatform.Aggregator.DTOs;

namespace MusicOrchestraPlatform.Aggregator.Clients.Interfaces
{
    public interface ICatalogClient
    {
        Task<ProductDto?> GetProductByIdAsync(int productId, CancellationToken cancellationToken = default);
    }
}
