using CatalogService.DAL.Data;
using CatalogService.DAL.Repositories.Interfaces;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.DAL.Repositories
{
    public class InstrumentImageRepository : GenericRepository<InstrumentImage>, IInstrumentImageRepository
    {
        private readonly CatalogDbContext _context;

        public InstrumentImageRepository(CatalogDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InstrumentImage>> GetAllAsync() =>
            await _context.InstrumentImages
                .Include(img => img.Instrument)
                .OrderBy(img => img.Id)
                .ToListAsync();

        public async Task<InstrumentImage?> GetByIdAsync(int id) =>
            await _context.InstrumentImages
                .Include(img => img.Instrument)
                .Where(img => img.Id == id)
                .FirstOrDefaultAsync();

        public async Task AddAsync(InstrumentImage entity)
        {
            await _context.InstrumentImages.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(InstrumentImage entity)
        {
            _context.InstrumentImages.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.InstrumentImages
                .Where(img => img.Id == id)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                _context.InstrumentImages.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
