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
    public class CompositionConfiguration : IEntityTypeConfiguration<Composition>
    {
        public void Configure(EntityTypeBuilder<Composition> builder)
        {
            builder.ToTable("Compositions");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Duration)
                .IsRequired();

            builder.Property(c => c.Genre)
                .HasMaxLength(50);

            builder.HasData(
                new Composition { Id = 1, Title = "Symphony No. 5", Duration = 3600, Genre = "Classical" },
                new Composition { Id = 2, Title = "Jazz Improvisation", Duration = 1800, Genre = "Jazz" }
            );
        }
    }
}
