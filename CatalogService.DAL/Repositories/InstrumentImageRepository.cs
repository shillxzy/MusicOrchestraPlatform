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
    public class InstrumentImageRepository : IInstrumentImageRepository
    {
        private readonly CatalogDbContext _context;

        public InstrumentImageRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InstrumentImage>> GetAllAsync() =>
            await _context.InstrumentImages
                .Include(img => img.Instrument)
                .ToListAsync();

        public async Task<InstrumentImage?> GetByIdAsync(int id) =>
            await _context.InstrumentImages
                .Include(img => img.Instrument)
                .FirstOrDefaultAsync(img => img.Id == id);

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
            var entity = await _context.InstrumentImages.FindAsync(id);
            if (entity != null)
            {
                _context.InstrumentImages.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
