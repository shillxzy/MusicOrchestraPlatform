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
    public class CompositionRepository : ICompositionRepository
    {
        private readonly CatalogDbContext _context;

        public CompositionRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Composition>> GetAllAsync() =>
            await _context.Compositions
                .Include(c => c.ConcertPrograms)
                .ToListAsync();

        public async Task<Composition?> GetByIdAsync(int id) =>
            await _context.Compositions
                .Include(c => c.ConcertPrograms)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task AddAsync(Composition entity)
        {
            await _context.Compositions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Composition entity)
        {
            _context.Compositions.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Compositions.FindAsync(id);
            if (entity != null)
            {
                _context.Compositions.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
