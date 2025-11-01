using CatalogService.DAL.Data;
using CatalogService.DAL.Repositories.Interfaces;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;


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
                .ToListAsync();

        public async Task<Instrument?> GetByIdAsync(int id) =>
            await _context.Instruments
                .Include(i => i.Performers)
                .Include(i => i.InstrumentImage)
                .FirstOrDefaultAsync(i => i.Id == id);

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
            var entity = await _context.Instruments.FindAsync(id);
            if (entity != null)
            {
                _context.Instruments.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
