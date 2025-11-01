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
    public class PerformerRepository : IPerformerRepository
    {
        private readonly CatalogDbContext _context;

        public PerformerRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Performer>> GetAllAsync() =>
            await _context.Performers
                .Include(p => p.Instrument)
                .ToListAsync();

        public async Task<Performer?> GetByIdAsync(int id) =>
            await _context.Performers
                .Include(p => p.Instrument)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddAsync(Performer entity)
        {
            await _context.Performers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Performer entity)
        {
            _context.Performers.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Performers.FindAsync(id);
            if (entity != null)
            {
                _context.Performers.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
