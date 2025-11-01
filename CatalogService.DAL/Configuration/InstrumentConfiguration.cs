using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DAL.Configuration
{
    public class InstrumentConfiguration : IEntityTypeConfiguration<Instrument>
    {
        public void Configure(EntityTypeBuilder<Instrument> builder)
        {
            builder.ToTable("Instruments");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.Price)
                .IsRequired()
                .HasPrecision(10, 2);

            // Перевірка, щоб ціна була > 0
            builder.ToTable(t =>
            {
                t.HasCheckConstraint("CK_Instrument_Price", "Price > 0");
            });


            // 1:1 → Instrument ↔ InstrumentImage
            builder.HasOne(i => i.InstrumentImage)
                   .WithOne(img => img.Instrument)
                   .HasForeignKey<InstrumentImage>(img => img.InstrumentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 1:N → Instrument → Performer
            builder.HasMany(i => i.Performers)
                   .WithOne(p => p.Instrument)
                   .HasForeignKey(p => p.InstrumentId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Seed
            builder.HasData(
                new Instrument { Id = 1, Name = "Violin", Type = "String", Price = 1200 },
                new Instrument { Id = 2, Name = "Trumpet", Type = "Brass", Price = 900 },
                new Instrument { Id = 3, Name = "Drum", Type = "Percussion", Price = 1500 }
            );
        }
    }
}
