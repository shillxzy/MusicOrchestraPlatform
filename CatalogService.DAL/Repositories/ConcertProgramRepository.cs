using CatalogService.DAL.Data;
using CatalogService.DAL.Repositories.Interfaces;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.DAL.Repositories
{
    public class ConcertProgramRepository : GenericRepository<ConcertProgram>, IConcertProgramRepository
    {
        private readonly CatalogDbContext _context;

        public ConcertProgramRepository(CatalogDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConcertProgram>> GetAllAsync() =>
            await _context.ConcertPrograms
                .Include(cp => cp.Composition)
                .OrderBy(cp => cp.Id) 
                .ToListAsync();

        public async Task<ConcertProgram?> GetByIdAsync(int id) =>
            await _context.ConcertPrograms
                .Include(cp => cp.Composition)
                .Where(cp => cp.Id == id)
                .FirstOrDefaultAsync();

        public async Task AddAsync(ConcertProgram entity)
        {
            await _context.ConcertPrograms.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ConcertProgram entity)
        {
            _context.ConcertPrograms.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ConcertPrograms
                .Where(cp => cp.Id == id)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                _context.ConcertPrograms.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
