using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) {}

        // Define DbSets here when ready
        // Example:
        // public DbSet<Movie> Movies { get; set; }
    }
}
