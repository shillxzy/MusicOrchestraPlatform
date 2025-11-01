using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.DAL.Repositories.Interfaces
{
    public interface ICompositionRepository
    {
        Task<IEnumerable<Composition>> GetAllAsync();
        Task<Composition?> GetByIdAsync(int id);
        Task AddAsync(Composition entity);
        Task UpdateAsync(Composition entity);
        Task DeleteAsync(int id);
    }
}
