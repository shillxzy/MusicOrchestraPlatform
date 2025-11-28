using CatalogService.DAL.Data;
using CatalogService.DAL.Repositories.Interfaces;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.DAL.Repositories
{
    public class PerformerRepository : GenericRepository<Performer>, IPerformerRepository
    {
        private readonly CatalogDbContext _context;

        public PerformerRepository(CatalogDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Performer>> GetAllAsync() =>
            await _context.Performers
                .Include(p => p.Instrument)
                .OrderBy(p => p.Id)
                .ToListAsync();

        public async Task<Performer?> GetByIdAsync(int id) =>
            await _context.Performers
                .Include(p => p.Instrument)
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

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
            var entity = await _context.Performers
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                _context.Performers.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
