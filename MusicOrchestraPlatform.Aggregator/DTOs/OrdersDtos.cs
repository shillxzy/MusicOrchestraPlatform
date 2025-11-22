namespace MusicOrchestraPlatform.Aggregator.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
    }

    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public CustomerDto? Customer { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
