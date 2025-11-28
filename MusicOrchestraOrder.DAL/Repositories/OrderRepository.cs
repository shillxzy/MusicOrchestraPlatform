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
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly IConnectFactory _connectionFactory;

        public OrderRepository(IConnectFactory connectionFactory) : base(connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                SELECT Id, CustomerId, TotalAmount, Status, CreatedAt, UpdatedAt 
                FROM Orders 
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
                return new Order
                {
                    Id = reader.GetInt32("Id"),
                    CustomerId = reader.GetInt32("CustomerId"),
                    TotalAmount = reader.GetDecimal("TotalAmount"),
                    Status = reader.GetString("Status"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),
                    UpdatedAt = reader.IsDBNull("UpdatedAt") ? null : reader.GetDateTime("UpdatedAt")
                };
            }

            return null;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                SELECT Id, CustomerId, TotalAmount, Status, CreatedAt, UpdatedAt 
                FROM Orders 
                ORDER BY CreatedAt DESC";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            var orders = new List<Order>();
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                orders.Add(new Order
                {
                    Id = reader.GetInt32("Id"),
                    CustomerId = reader.GetInt32("CustomerId"),
                    TotalAmount = reader.GetDecimal("TotalAmount"),
                    Status = reader.GetString("Status"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),
                    UpdatedAt = reader.IsDBNull("UpdatedAt") ? null : reader.GetDateTime("UpdatedAt")
                });
            }

            return orders;
        }

        public async Task<int> AddAsync(Order order)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                INSERT INTO Orders (CustomerId, TotalAmount, Status, CreatedAt, UpdatedAt) 
                VALUES (@CustomerId, @TotalAmount, @Status, @CreatedAt, @UpdatedAt) 
                RETURNING Id";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            var customerIdParam = command.CreateParameter();
            customerIdParam.ParameterName = "@CustomerId";
            customerIdParam.Value = order.CustomerId;
            command.Parameters.Add(customerIdParam);

            var totalAmountParam = command.CreateParameter();
            totalAmountParam.ParameterName = "@TotalAmount";
            totalAmountParam.Value = order.TotalAmount;
            command.Parameters.Add(totalAmountParam);

            var statusParam = command.CreateParameter();
            statusParam.ParameterName = "@Status";
            statusParam.Value = order.Status;
            command.Parameters.Add(statusParam);

            var createdAtParam = command.CreateParameter();
            createdAtParam.ParameterName = "@CreatedAt";
            createdAtParam.Value = order.CreatedAt;
            command.Parameters.Add(createdAtParam);

            var updatedAtParam = command.CreateParameter();
            updatedAtParam.ParameterName = "@UpdatedAt";
            updatedAtParam.Value = order.UpdatedAt ?? (object)DBNull.Value;
            command.Parameters.Add(updatedAtParam);

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task UpdateAsync(Order order)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                UPDATE Orders 
                SET CustomerId = @CustomerId, TotalAmount = @TotalAmount, Status = @Status, UpdatedAt = @UpdatedAt 
                WHERE Id = @Id";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            var idParam = command.CreateParameter();
            idParam.ParameterName = "@Id";
            idParam.Value = order.Id;
            command.Parameters.Add(idParam);

            var customerIdParam = command.CreateParameter();
            customerIdParam.ParameterName = "@CustomerId";
            customerIdParam.Value = order.CustomerId;
            command.Parameters.Add(customerIdParam);

            var totalAmountParam = command.CreateParameter();
            totalAmountParam.ParameterName = "@TotalAmount";
            totalAmountParam.Value = order.TotalAmount;
            command.Parameters.Add(totalAmountParam);

            var statusParam = command.CreateParameter();
            statusParam.ParameterName = "@Status";
            statusParam.Value = order.Status;
            command.Parameters.Add(statusParam);

            var updatedAtParam = command.CreateParameter();
            updatedAtParam.ParameterName = "@UpdatedAt";
            updatedAtParam.Value = order.UpdatedAt ?? DateTime.UtcNow;
            command.Parameters.Add(updatedAtParam);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = "DELETE FROM Orders WHERE Id = @Id";

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