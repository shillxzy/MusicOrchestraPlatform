using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.DAL.Data
{
    public class CatalogDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
    {
        public CatalogDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>();
            optionsBuilder.UseNpgsql("Host=ep-muddy-rice-a4125q9t-pooler.us-east-1.aws.neon.tech;Database=music_orchestra_catalog;Username=neondb_owner;Password=npg_hsRQ3OtXoUG4;SSL Mode=Require");

            return new CatalogDbContext(optionsBuilder.Options);
        }
    }
}
