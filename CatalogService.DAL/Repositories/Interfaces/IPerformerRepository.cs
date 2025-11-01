using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogService.Domain.Entities;

namespace CatalogService.DAL.Repositories.Interfaces
{
    public interface IPerformerRepository
    {
        Task<IEnumerable<Performer>> GetAllAsync();
        Task<Performer?> GetByIdAsync(int id);
        Task AddAsync(Performer entity);
        Task UpdateAsync(Performer entity);
        Task DeleteAsync(int id);
    }
}
