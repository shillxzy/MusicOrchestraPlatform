using MusicOrchestraPlatform.Aggregator.DTOs;

namespace MusicOrchestraPlatform.Aggregator.Clients.Interfaces
{
    public interface IOrdersClient
    {
        Task<OrderDto?> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken = default);
    }
}
