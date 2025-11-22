using Microsoft.AspNetCore.Mvc;
using MusicOrchestraPlatform.Aggregator.Clients.Interfaces;
using MusicOrchestraPlatform.Aggregator.DTOs;

namespace MusicOrchestraPlatform.Aggregator.Controllers
{
    [ApiController]
    [Route("api/aggregator/orders")]
    public class AggregatedOrdersController : ControllerBase
    {
        private readonly IOrdersClient _ordersClient;
        private readonly ICatalogClient _catalogClient;
        private readonly IReviewsClient _reviewsClient;
        private readonly ILogger<AggregatedOrdersController> _logger;

        public AggregatedOrdersController(
            IOrdersClient ordersClient,
            ICatalogClient catalogClient,
            IReviewsClient reviewsClient,
            ILogger<AggregatedOrdersController> logger)
        {
            _ordersClient = ordersClient;
            _catalogClient = catalogClient;
            _reviewsClient = reviewsClient;
            _logger = logger;
        }

        [HttpGet("{orderId}/full")]
        public async Task<IActionResult> GetAggregatedOrder(int orderId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching aggregated order {OrderId}", orderId);

            var order = await _ordersClient.GetOrderByIdAsync(orderId, cancellationToken);
            if (order == null)
            {
                _logger.LogWarning("Order {OrderId} not found", orderId);
                return NotFound();
            }

            var aggregatedOrder = new AggregatedOrderDto
            {
                OrderId = order.Id,
                OrderDate = order.CreatedAt,
                CustomerId = order.CustomerId,
                CustomerName = order.Customer?.FullName ?? "Unknown"
            };

            // Паралельне отримання всіх продуктів та рев’ю
            var productTasks = order.Items.Select(item => _catalogClient.GetProductByIdAsync(item.ProductId, cancellationToken)).ToArray();
            var reviewTasks = order.Items.Select(item => _reviewsClient.GetReviewByProductIdAsync(item.ProductId, cancellationToken)).ToArray();

            var products = await Task.WhenAll(productTasks);
            var reviews = await Task.WhenAll(reviewTasks);

            for (int i = 0; i < order.Items.Count; i++)
            {
                var item = order.Items[i];
                var product = products[i];

                aggregatedOrder.Items.Add(new OrderItemDto
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                    // Можна додати інші поля при необхідності
                });

                var review = reviews[i];
                if (review != null)
                {
                    aggregatedOrder.Reviews.Add(new ReviewDto
                    {
                        Id = review.Id,
                        ProductId = review.ProductId,
                        Rating = review.Rating,
                        Text = review.Text,
                        Discussions = review.Discussions
                    });
                }
            }

            _logger.LogInformation("Aggregated order {OrderId} prepared with {ItemsCount} items and {ReviewsCount} reviews",
                orderId, aggregatedOrder.Items.Count, aggregatedOrder.Reviews.Count);

            return Ok(aggregatedOrder);
        }
    }
}
