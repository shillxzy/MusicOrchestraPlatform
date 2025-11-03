using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Infrastructure.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IClientSessionHandle Session { get; }
        Task StartTransactionAsync();
        Task CommitAsync(CancellationToken cancellationToken = default);
        Task AbortAsync(CancellationToken cancellationToken = default);
    }
}
