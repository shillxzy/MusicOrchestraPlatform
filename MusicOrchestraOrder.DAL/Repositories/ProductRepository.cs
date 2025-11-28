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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IConnectFactory _connectionFactory;

        public ProductRepository(IConnectFactory connectionFactory) : base(connectionFactory) 
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                SELECT Id, Name, Price, CreatedAt, UpdatedAt 
                FROM Products 
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
                return new Product
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Price = reader.GetDecimal("Price"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),
                    UpdatedAt = reader.IsDBNull("UpdatedAt") ? null : reader.GetDateTime("UpdatedAt")
                };
            }

            return null;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                SELECT Id, Name, Price, CreatedAt, UpdatedAt 
                FROM Products 
                ORDER BY CreatedAt DESC";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            var products = new List<Product>();
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                products.Add(new Product
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Price = reader.GetDecimal("Price"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),
                    UpdatedAt = reader.IsDBNull("UpdatedAt") ? null : reader.GetDateTime("UpdatedAt")
                });
            }

            return products;
        }

        public async Task<int> AddAsync(Product product)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                INSERT INTO Products (Name, Price, CreatedAt, UpdatedAt) 
                VALUES (@Name, @Price, @CreatedAt, @UpdatedAt) 
                RETURNING Id";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            var nameParam = command.CreateParameter();
            nameParam.ParameterName = "@Name";
            nameParam.Value = product.Name;
            command.Parameters.Add(nameParam);

            var priceParam = command.CreateParameter();
            priceParam.ParameterName = "@Price";
            priceParam.Value = product.Price;
            command.Parameters.Add(priceParam);

            var createdAtParam = command.CreateParameter();
            createdAtParam.ParameterName = "@CreatedAt";
            createdAtParam.Value = product.CreatedAt;
            command.Parameters.Add(createdAtParam);

            var updatedAtParam = command.CreateParameter();
            updatedAtParam.ParameterName = "@UpdatedAt";
            updatedAtParam.Value = product.UpdatedAt ?? (object)DBNull.Value;
            command.Parameters.Add(updatedAtParam);

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task UpdateAsync(Product product)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                UPDATE Products 
                SET Name = @Name, Price = @Price, UpdatedAt = @UpdatedAt 
                WHERE Id = @Id";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            var idParam = command.CreateParameter();
            idParam.ParameterName = "@Id";
            idParam.Value = product.Id;
            command.Parameters.Add(idParam);

            var nameParam = command.CreateParameter();
            nameParam.ParameterName = "@Name";
            nameParam.Value = product.Name;
            command.Parameters.Add(nameParam);

            var priceParam = command.CreateParameter();
            priceParam.ParameterName = "@Price";
            priceParam.Value = product.Price;
            command.Parameters.Add(priceParam);

            var updatedAtParam = command.CreateParameter();
            updatedAtParam.ParameterName = "@UpdatedAt";
            updatedAtParam.Value = product.UpdatedAt ?? DateTime.UtcNow;
            command.Parameters.Add(updatedAtParam);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = "DELETE FROM Products WHERE Id = @Id";

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