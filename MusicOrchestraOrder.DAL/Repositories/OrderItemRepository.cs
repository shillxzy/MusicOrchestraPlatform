using OrderService.DAL.Infrastructure;
using OrderService.DAL.Repositories.Interfaces;
using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace OrderService.DAL.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        private readonly IConnectFactory _connectionFactory;

        public OrderItemRepository(IConnectFactory connectionFactory) : base(connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                SELECT Id, OrderId, ProductId, Quantity, UnitPrice 
                FROM OrderItems 
                WHERE Id = @Id";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            var idParam = command.CreateParameter();
            idParam.ParameterName = "@Id";
            idParam.Value = id;
            command.Parameters.Add(idParam);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new OrderItem
                {
                    Id = reader.GetInt32("Id"),
                    OrderId = reader.GetInt32("OrderId"),
                    ProductId = reader.GetInt32("ProductId"),
                    Quantity = reader.GetInt32("Quantity"),
                    UnitPrice = reader.GetDecimal("UnitPrice")
                };
            }

            return null;
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                SELECT Id, OrderId, ProductId, Quantity, UnitPrice 
                FROM OrderItems 
                WHERE OrderId = @OrderId
                ORDER BY Id";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            var orderIdParam = command.CreateParameter();
            orderIdParam.ParameterName = "@OrderId";
            orderIdParam.Value = orderId;
            command.Parameters.Add(orderIdParam);

            var orderItems = new List<OrderItem>();
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                orderItems.Add(new OrderItem
                {
                    Id = reader.GetInt32("Id"),
                    OrderId = reader.GetInt32("OrderId"),
                    ProductId = reader.GetInt32("ProductId"),
                    Quantity = reader.GetInt32("Quantity"),
                    UnitPrice = reader.GetDecimal("UnitPrice")
                });
            }

            return orderItems;
        }

        public async Task<int> AddAsync(OrderItem item)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice) 
                VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice) 
                RETURNING Id";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            var orderIdParam = command.CreateParameter();
            orderIdParam.ParameterName = "@OrderId";
            orderIdParam.Value = item.OrderId;
            command.Parameters.Add(orderIdParam);

            var productIdParam = command.CreateParameter();
            productIdParam.ParameterName = "@ProductId";
            productIdParam.Value = item.ProductId;
            command.Parameters.Add(productIdParam);

            var quantityParam = command.CreateParameter();
            quantityParam.ParameterName = "@Quantity";
            quantityParam.Value = item.Quantity;
            command.Parameters.Add(quantityParam);

            var unitPriceParam = command.CreateParameter();
            unitPriceParam.ParameterName = "@UnitPrice";
            unitPriceParam.Value = item.UnitPrice;
            command.Parameters.Add(unitPriceParam);

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task UpdateAsync(OrderItem item)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                UPDATE OrderItems 
                SET OrderId = @OrderId, ProductId = @ProductId, Quantity = @Quantity, UnitPrice = @UnitPrice 
                WHERE Id = @Id";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            var idParam = command.CreateParameter();
            idParam.ParameterName = "@Id";
            idParam.Value = item.Id;
            command.Parameters.Add(idParam);

            var orderIdParam = command.CreateParameter();
            orderIdParam.ParameterName = "@OrderId";
            orderIdParam.Value = item.OrderId;
            command.Parameters.Add(orderIdParam);

            var productIdParam = command.CreateParameter();
            productIdParam.ParameterName = "@ProductId";
            productIdParam.Value = item.ProductId;
            command.Parameters.Add(productIdParam);

            var quantityParam = command.CreateParameter();
            quantityParam.ParameterName = "@Quantity";
            quantityParam.Value = item.Quantity;
            command.Parameters.Add(quantityParam);

            var unitPriceParam = command.CreateParameter();
            unitPriceParam.ParameterName = "@UnitPrice";
            unitPriceParam.Value = item.UnitPrice;
            command.Parameters.Add(unitPriceParam);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = "DELETE FROM OrderItems WHERE Id = @Id";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            var idParam = command.CreateParameter();
            idParam.ParameterName = "@Id";
            idParam.Value = id;
            command.Parameters.Add(idParam);

            await command.ExecuteNonQueryAsync();
        }
    }
}