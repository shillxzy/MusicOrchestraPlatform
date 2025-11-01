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
    public class ConcertProgramConfiguration : IEntityTypeConfiguration<ConcertProgram>
    {
        public void Configure(EntityTypeBuilder<ConcertProgram> builder)
        {
            builder.ToTable("ConcertPrograms");

            builder.HasKey(cp => cp.Id);

            builder.HasOne(cp => cp.Composition)
                   .WithMany()
                   .HasForeignKey(cp => cp.CompositionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(cp => new { cp.ConcertId, cp.CompositionId }).IsUnique();

            builder.HasData(
                new ConcertProgram { Id = 1, ConcertId = 1, CompositionId = 1 },
                new ConcertProgram { Id = 2, ConcertId = 1, CompositionId = 2 }
            );
        }
    }
}
