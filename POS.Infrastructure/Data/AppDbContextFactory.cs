using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;
namespace POS.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // IMPORTANT: match your runtime DB path
            var dbPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Database",
                "pos.db"
            );

            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}