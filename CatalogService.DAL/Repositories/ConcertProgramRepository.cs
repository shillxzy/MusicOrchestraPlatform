using CatalogService.DAL.Data;
using CatalogService.DAL.Repositories.Interfaces;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.DAL.Repositories
{
    public class ConcertProgramRepository : IConcertProgramRepository
    {
        private readonly CatalogDbContext _context;

        public ConcertProgramRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConcertProgram>> GetAllAsync() =>
            await _context.ConcertPrograms
                .Include(cp => cp.Composition)
                .ToListAsync();

        public async Task<ConcertProgram?> GetByIdAsync(int id) =>
            await _context.ConcertPrograms
                .Include(cp => cp.Composition)
                .FirstOrDefaultAsync(cp => cp.Id == id);

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
            var entity = await _context.ConcertPrograms.FindAsync(id);
            if (entity != null)
            {
                _context.ConcertPrograms.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
