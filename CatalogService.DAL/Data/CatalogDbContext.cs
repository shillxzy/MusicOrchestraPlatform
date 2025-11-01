using Microsoft.EntityFrameworkCore;
using CatalogService.Domain.Entities;

namespace CatalogService.DAL.Data
{
    public class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
    {
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Performer> Performers { get; set; }
        public DbSet<Composition> Compositions { get; set; }
        public DbSet<ConcertProgram> ConcertPrograms { get; set; }
        public DbSet<InstrumentImage> InstrumentImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        }
    }
}
