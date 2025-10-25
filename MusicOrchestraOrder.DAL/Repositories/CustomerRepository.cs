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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConnectFactory _connectionFactory;

        public CustomerRepository(IConnectFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                SELECT Id, Name, Email, CreatedAt, UpdatedAt 
                FROM Customers 
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
                return new Customer
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Email = reader.GetString("Email"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),
                    UpdatedAt = reader.IsDBNull("UpdatedAt") ? null : reader.GetDateTime("UpdatedAt")
                };
            }

            return null;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                SELECT Id, Name, Email, CreatedAt, UpdatedAt 
                FROM Customers 
                ORDER BY CreatedAt DESC";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            var customers = new List<Customer>();
            using var reader = await command.ExecuteReaderAsync();
            
            while (await reader.ReadAsync())
            {
                customers.Add(new Customer
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Email = reader.GetString("Email"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),
                    UpdatedAt = reader.IsDBNull("UpdatedAt") ? null : reader.GetDateTime("UpdatedAt")
                });
            }

            return customers;
        }

        public async Task<int> AddAsync(Customer customer)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                INSERT INTO Customers (Name, Email, CreatedAt, UpdatedAt) 
                VALUES (@Name, @Email, @CreatedAt, @UpdatedAt) 
                RETURNING Id";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            var nameParam = command.CreateParameter();
            nameParam.ParameterName = "@Name";
            nameParam.Value = customer.Name;
            command.Parameters.Add(nameParam);

            var emailParam = command.CreateParameter();
            emailParam.ParameterName = "@Email";
            emailParam.Value = customer.Email;
            command.Parameters.Add(emailParam);

            var createdAtParam = command.CreateParameter();
            createdAtParam.ParameterName = "@CreatedAt";
            createdAtParam.Value = customer.CreatedAt;
            command.Parameters.Add(createdAtParam);

            var updatedAtParam = command.CreateParameter();
            updatedAtParam.ParameterName = "@UpdatedAt";
            updatedAtParam.Value = customer.UpdatedAt ?? (object)DBNull.Value;
            command.Parameters.Add(updatedAtParam);

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task UpdateAsync(Customer customer)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"
                UPDATE Customers 
                SET Name = @Name, Email = @Email, UpdatedAt = @UpdatedAt 
                WHERE Id = @Id";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            var idParam = command.CreateParameter();
            idParam.ParameterName = "@Id";
            idParam.Value = customer.Id;
            command.Parameters.Add(idParam);

            var nameParam = command.CreateParameter();
            nameParam.ParameterName = "@Name";
            nameParam.Value = customer.Name;
            command.Parameters.Add(nameParam);

            var emailParam = command.CreateParameter();
            emailParam.ParameterName = "@Email";
            emailParam.Value = customer.Email;
            command.Parameters.Add(emailParam);

            var updatedAtParam = command.CreateParameter();
            updatedAtParam.ParameterName = "@UpdatedAt";
            updatedAtParam.Value = customer.UpdatedAt ?? DateTime.UtcNow;
            command.Parameters.Add(updatedAtParam);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = "DELETE FROM Customers WHERE Id = @Id";

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