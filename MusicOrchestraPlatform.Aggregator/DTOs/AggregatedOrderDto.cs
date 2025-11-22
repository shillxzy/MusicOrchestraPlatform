namespace MusicOrchestraPlatform.Aggregator.DTOs
{
    public class AggregatedOrderDto
    {
        // Інформація про замовлення
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        // Список товарів у замовленні
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();

        // Додаткові рев’ю для замовлення/товарів
        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
    }
}
