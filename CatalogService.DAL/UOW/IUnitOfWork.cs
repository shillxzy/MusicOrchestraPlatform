using CatalogService.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.DAL.UOW
{
    public interface IUnitOfWork
    {
        IInstrumentRepository Instruments { get; }
        IPerformerRepository Performers { get; }
        ICompositionRepository Compositions { get; }
        IConcertProgramRepository ConcertPrograms { get; }
        IInstrumentImageRepository InstrumentImages { get; }

        Task<int> SaveChangesAsync();
    }
}
