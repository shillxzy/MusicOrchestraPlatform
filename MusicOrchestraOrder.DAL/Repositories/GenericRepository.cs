using Npgsql;
using OrderService.DAL.Infrastructure;
using OrderService.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;

namespace OrderService.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly IConnectFactory _connectionFactory;
        private readonly string _tableName;

        public GenericRepository(IConnectFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _tableName = typeof(T).Name + "s"; 
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = $"SELECT * FROM {_tableName} WHERE Id = @Id";

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
                return MapReaderToEntity(reader);
            }

            return null;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = $"SELECT * FROM {_tableName}";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;

            var entities = new List<T>();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                entities.Add(MapReaderToEntity(reader));
            }

            return entities;
        }

        public async Task<int> AddAsync(T entity)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var properties = typeof(T).GetProperties();
            var columns = new List<string>();
            var values = new List<string>();

            foreach (var prop in properties)
            {
                if (prop.Name == "Id") continue; // Id генерується базою
                columns.Add(prop.Name);
                values.Add($"@{prop.Name}");
            }

            var query = $"INSERT INTO {_tableName} ({string.Join(",", columns)}) VALUES ({string.Join(",", values)}) RETURNING Id";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;

            foreach (var prop in properties)
            {
                if (prop.Name == "Id") continue;
                var param = command.CreateParameter();
                param.ParameterName = $"@{prop.Name}";
                var value = prop.GetValue(entity) ?? DBNull.Value;
                param.Value = value;
                command.Parameters.Add(param);
            }

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task UpdateAsync(T entity)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var properties = typeof(T).GetProperties();
            var setParts = new List<string>();
            int id = 0;

            foreach (var prop in properties)
            {
                if (prop.Name == "Id")
                {
                    id = (int)prop.GetValue(entity)!;
                    continue;
                }
                setParts.Add($"{prop.Name} = @{prop.Name}");
            }

            var query = $"UPDATE {_tableName} SET {string.Join(",", setParts)} WHERE Id = @Id";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;

            foreach (var prop in properties)
            {
                var param = command.CreateParameter();
                param.ParameterName = $"@{prop.Name}";
                param.Value = prop.GetValue(entity) ?? DBNull.Value;
                command.Parameters.Add(param);
            }

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = (NpgsqlConnection)_connectionFactory.CreateConnection();
            await connection.OpenAsync();

            var query = $"DELETE FROM {_tableName} WHERE Id = @Id";

            using var command = (NpgsqlCommand)connection.CreateCommand();
            command.CommandText = query;

            var idParam = command.CreateParameter();
            idParam.ParameterName = "@Id";
            idParam.Value = id;
            command.Parameters.Add(idParam);

            await command.ExecuteNonQueryAsync();
        }

        private T MapReaderToEntity(NpgsqlDataReader reader)
        {
            var entity = new T();
            var props = typeof(T).GetProperties();

            foreach (var prop in props)
            {
                if (!reader.HasColumn(prop.Name)) continue;

                var value = reader[prop.Name] == DBNull.Value ? null : reader[prop.Name];
                prop.SetValue(entity, value);
            }

            return entity;
        }
    }

    public static class NpgsqlDataReaderExtensions
    {
        public static bool HasColumn(this NpgsqlDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
