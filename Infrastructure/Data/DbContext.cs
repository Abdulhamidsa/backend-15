using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Title> Titles { get; set; }
        public DbSet<User> Users  => Set<User>();
        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Name> Names { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            
        }
    }
}