using Dapper;
using Npgsql;
using OrderService.DAL.Infrastructure;
using OrderService.DAL.Repositories.Interfaces;
using OrderService.Domain.Entities;


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

            var query = @"SELECT Id, Name, Email, CreatedAt, UpdatedAt 
                          FROM Customers 
                          WHERE Id = @Id";

            // Dapper сам мапить результат в Customer
            return await connection.QueryFirstOrDefaultAsync<Customer>(query, new { Id = id });
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"SELECT Id, Name, Email, CreatedAt, UpdatedAt 
                          FROM Customers 
                          ORDER BY CreatedAt DESC";

            return await connection.QueryAsync<Customer>(query);
        }

        public async Task<int> AddAsync(Customer customer)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"INSERT INTO Customers (Name, Email, CreatedAt, UpdatedAt) 
                          VALUES (@Name, @Email, @CreatedAt, @UpdatedAt) 
                          RETURNING Id";

            return await connection.ExecuteScalarAsync<int>(query, new
            {
                customer.Name,
                customer.Email,
                customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt
            });
        }

        public async Task UpdateAsync(Customer customer)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"UPDATE Customers 
                          SET Name = @Name, Email = @Email, UpdatedAt = @UpdatedAt 
                          WHERE Id = @Id";

            await connection.ExecuteAsync(query, new
            {
                customer.Id,
                customer.Name,
                customer.Email,
                UpdatedAt = customer.UpdatedAt ?? System.DateTime.UtcNow
            });
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = @"DELETE FROM Customers WHERE Id = @Id";

            await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
