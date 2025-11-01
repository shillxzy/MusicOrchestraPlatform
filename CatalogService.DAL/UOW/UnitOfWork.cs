using CatalogService.DAL.Data;
using CatalogService.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.DAL.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogDbContext _context;

        public IInstrumentRepository Instruments { get; }
        public IPerformerRepository Performers { get; }
        public ICompositionRepository Compositions { get; }
        public IConcertProgramRepository ConcertPrograms { get; }
        public IInstrumentImageRepository InstrumentImages { get; }

        public UnitOfWork(
            CatalogDbContext context,
            IInstrumentRepository instrumentRepository,
            IPerformerRepository performerRepository,
            ICompositionRepository compositionRepository,
            IConcertProgramRepository concertProgramRepository,
            IInstrumentImageRepository instrumentImageRepository)
        {
            _context = context;

            Instruments = instrumentRepository;
            Performers = performerRepository;
            Compositions = compositionRepository;
            ConcertPrograms = concertProgramRepository;
            InstrumentImages = instrumentImageRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
