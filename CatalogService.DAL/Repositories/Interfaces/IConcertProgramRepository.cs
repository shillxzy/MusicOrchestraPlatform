using CatalogService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.DAL.Repositories.Interfaces
{
    public interface IConcertProgramRepository
    {
        Task<IEnumerable<ConcertProgram>> GetAllAsync();
        Task<ConcertProgram?> GetByIdAsync(int id);
        Task AddAsync(ConcertProgram entity);
        Task UpdateAsync(ConcertProgram entity);
        Task DeleteAsync(int id);
    }
}
