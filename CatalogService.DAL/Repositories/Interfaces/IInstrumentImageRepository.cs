using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.DAL.Repositories.Interfaces
{
    public interface IInstrumentImageRepository
    {
        Task<IEnumerable<InstrumentImage>> GetAllAsync();
        Task<InstrumentImage?> GetByIdAsync(int id);
        Task AddAsync(InstrumentImage entity);
        Task UpdateAsync(InstrumentImage entity);
        Task DeleteAsync(int id);
    }
}
