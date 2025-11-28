using CatalogService.DAL.Data;
using CatalogService.DAL.Repositories.Interfaces;
using CatalogService.DAL.Specifications;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.DAL.Repositories
{
    public class InstrumentRepository : GenericRepository<Instrument>, IInstrumentRepository
    {
        private readonly CatalogDbContext _context;

        public InstrumentRepository(CatalogDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Instrument>> GetAllAsync() =>
            await _context.Instruments
                .Include(i => i.Performers)
                .Include(i => i.InstrumentImage)
                .OrderBy(i => i.Id)
                .ToListAsync();

        public async Task<Instrument?> GetByIdAsync(int id) =>
            await _context.Instruments
                .Include(i => i.Performers)
                .Include(i => i.InstrumentImage)
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

        public async Task AddAsync(Instrument entity)
        {
            await _context.Instruments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Instrument entity)
        {
            _context.Instruments.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Instruments
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                _context.Instruments.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Instrument>> GetBySpecificationAsync(ISpecification<Instrument> spec)
        {
            return await _context.Instruments
                .Include(i => i.InstrumentImage)
                .Include(i => i.Performers)
                .Where(spec.ToExpression())
                .ToListAsync();
        }
    }
}
