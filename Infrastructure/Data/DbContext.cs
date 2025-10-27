using Application.DTOs;
using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Title> Titles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<TitleSearchRow> TitleSearchRows => Set<TitleSearchRow>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<TitleSearchRow>(eb =>
            {
                eb.HasNoKey();
                eb.ToView(null); // it’s a function/projection, not a mapped view/table
                eb.Property(p => p.Tconst).HasColumnName("tconst");
                eb.Property(p => p.PrimaryTitle).HasColumnName("primarytitle");
            });
        }
    }
}