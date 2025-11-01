using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.DAL.Repositories.Interfaces
{
    public interface IInstrumentRepository : IGenericRepository<Instrument>
    {
        Task<IEnumerable<Instrument>> GetAllAsync();
        Task<Instrument?> GetByIdAsync(int id);
        Task AddAsync(Instrument entity);
        Task UpdateAsync(Instrument entity);
        Task DeleteAsync(int id);
    }
}
