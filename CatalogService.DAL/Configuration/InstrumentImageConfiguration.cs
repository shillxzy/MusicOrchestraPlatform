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
    public class InstrumentImageConfiguration : IEntityTypeConfiguration<InstrumentImage>
    {
        public void Configure(EntityTypeBuilder<InstrumentImage> builder)
        {
            builder.ToTable("InstrumentImages");

            builder.HasKey(ii => ii.Id);

            builder.Property(ii => ii.Url)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(ii => ii.Instrument)
                   .WithOne(i => i.InstrumentImage)
                   .HasForeignKey<InstrumentImage>(ii => ii.InstrumentId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new InstrumentImage { Id = 1, InstrumentId = 1, Url = "violin.jpg" },
                new InstrumentImage { Id = 2, InstrumentId = 2, Url = "trumpet.jpg" }
            );
        }
    }
}
