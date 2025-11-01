using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DAL.Data
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instrument>().HasData(
                new Instrument { Id = 1, Name = "Violin", Type = "String", Price = 500.00m },
                new Instrument { Id = 2, Name = "Trumpet", Type = "Brass", Price = 350.00m },
                new Instrument { Id = 3, Name = "Flute", Type = "Woodwind", Price = 270.00m }
            );

            modelBuilder.Entity<Performer>().HasData(
                new Performer { Id = 1, Name = "Anna Petrova", InstrumentId = 1 },
                new Performer { Id = 2, Name = "Maksym Ivanov", InstrumentId = 2 },
                new Performer { Id = 3, Name = "Oleh Kovalenko", InstrumentId = 3 }
            );

            modelBuilder.Entity<Composition>().HasData(
                new Composition { Id = 1, Title = "Symphony No.5", Duration = 45, Genre = "Classical" },
                new Composition { Id = 2, Title = "Carmen Suite", Duration = 25, Genre = "Opera" }
            );

            modelBuilder.Entity<ConcertProgram>().HasData(
                new ConcertProgram { Id = 1, ConcertId = 101, CompositionId = 1 },
                new ConcertProgram { Id = 2, ConcertId = 101, CompositionId = 2 }
            );

            modelBuilder.Entity<InstrumentImage>().HasData(
                new InstrumentImage { Id = 1, InstrumentId = 1, Url = "/images/violin.png" },
                new InstrumentImage { Id = 2, InstrumentId = 2, Url = "/images/trumpet.png" },
                new InstrumentImage { Id = 3, InstrumentId = 3, Url = "/images/flute.png" }
            );
        }
    }
}
