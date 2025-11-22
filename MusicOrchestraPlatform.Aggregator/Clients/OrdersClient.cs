using MusicOrchestraPlatform.Aggregator.Clients.Interfaces;
using MusicOrchestraPlatform.Aggregator.DTOs;

namespace MusicOrchestraPlatform.Aggregator.Clients
{
    public class OrdersClient : IOrdersClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<OrdersClient> _logger;

        public OrdersClient(HttpClient httpClient, ILogger<OrdersClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Calling Orders service for OrderId {OrderId}", orderId);
            var response = await _httpClient.GetAsync($"/api/orders/{orderId}", cancellationToken);
            response.EnsureSuccessStatusCode();
            var order = await response.Content.ReadFromJsonAsync<OrderDto>(cancellationToken: cancellationToken);
            _logger.LogInformation("Orders service responded for OrderId {OrderId}", orderId);
            return order;
        }
    }
}
