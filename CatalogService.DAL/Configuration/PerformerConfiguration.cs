using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.DAL.Configuration
{
    public class PerformerConfiguration : IEntityTypeConfiguration<Performer>
    {
        public void Configure(EntityTypeBuilder<Performer> builder)
        {
            builder.ToTable("Performers");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(p => p.Instrument)
                   .WithMany(i => i.Performers)
                   .HasForeignKey(p => p.InstrumentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Performer { Id = 1, Name = "John Doe", InstrumentId = 1 },
                new Performer { Id = 2, Name = "Maria Smith", InstrumentId = 2 }
            );
        }
    }
}
