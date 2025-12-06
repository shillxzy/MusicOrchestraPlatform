using Dapper;
using Npgsql;
using OrderService.DAL.Infrastructure;
using OrderService.DAL.Repositories.Interfaces;
using OrderService.Domain.Entities;


namespace OrderService.DAL.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly IConnectFactory _connectionFactory;

        public OrderItemRepository(IConnectFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"SELECT Id, OrderId, ProductId, Quantity, UnitPrice
                          FROM OrderItems
                          WHERE Id = @Id";

            return await connection.QueryFirstOrDefaultAsync<OrderItem>(query, new { Id = id });
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"SELECT Id, OrderId, ProductId, Quantity, UnitPrice
                          FROM OrderItems
                          WHERE OrderId = @OrderId
                          ORDER BY Id";

            return await connection.QueryAsync<OrderItem>(query, new { OrderId = orderId });
        }

        public async Task<int> AddAsync(OrderItem item)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice)
                          VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice)
                          RETURNING Id";

            return await connection.ExecuteScalarAsync<int>(query, new
            {
                item.OrderId,
                item.ProductId,
                item.Quantity,
                item.UnitPrice
            });
        }

        public async Task UpdateAsync(OrderItem item)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"UPDATE OrderItems
                          SET OrderId = @OrderId, ProductId = @ProductId, Quantity = @Quantity, UnitPrice = @UnitPrice
                          WHERE Id = @Id";

            await connection.ExecuteAsync(query, new
            {
                item.Id,
                item.OrderId,
                item.ProductId,
                item.Quantity,
                item.UnitPrice
            });
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"DELETE FROM OrderItems WHERE Id = @Id";

            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
