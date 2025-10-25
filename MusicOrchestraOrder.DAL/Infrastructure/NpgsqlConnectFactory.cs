using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace OrderService.DAL.Infrastructure
{
    public class NpgsqlConnectFactory : IConnectFactory
    {
        private readonly string _connectionString;

        public NpgsqlConnectFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
