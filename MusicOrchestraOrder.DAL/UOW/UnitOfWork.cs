using OrderService.DAL.Infrastructure;
using OrderService.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NpgsqlConnectFactory _connectionFactory;
        private IDbConnection? _connection;
        private IDbTransaction? _transaction;

        public ICustomerRepository Customers { get; }
        public IProductRepository Products { get; }
        public IOrderRepository Orders { get; }
        public IOrderItemRepository OrderItems { get; }

        public UnitOfWork(NpgsqlConnectFactory connectionFactory,
                          ICustomerRepository customers,
                          IProductRepository products,
                          IOrderRepository orders,
                          IOrderItemRepository orderItems)
        {
            _connectionFactory = connectionFactory;
            Customers = customers;
            Products = products;
            Orders = orders;
            OrderItems = orderItems;
        }

        public async Task BeginTransactionAsync()
        {
            _connection ??= _connectionFactory.CreateConnection();
            _transaction = _connection.BeginTransaction();
            await Task.CompletedTask;
        }

        public async Task CommitAsync()
        {
            _transaction?.Commit();
            await Task.CompletedTask;
        }

        public async Task RollbackAsync()
        {
            _transaction?.Rollback();
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }
    }
}
