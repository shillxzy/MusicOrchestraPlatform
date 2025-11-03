using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewievsService.Infrastructure.Indexes
{
    public interface IIndexCreationService
    {
        Task CreateIndexesAsync(CancellationToken cancellationToken = default);
    }
}
