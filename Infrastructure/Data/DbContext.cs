using Application.RowClasses;
using Domain.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Title> Titles { get; set; }
        public DbSet<User> Users => Set<User>();
        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<UserRating> Ratings { get; set; }

        public DbSet<Name> Names { get; set; }
        public DbSet<PersonMetadata> PersonMetadata { get; set; }
        // Add these:
        public DbSet<CelebritySummaryRow> CelebritySummaryRows => Set<CelebritySummaryRow>();
        public DbSet<CelebrityProfileRow> CelebrityProfileRows => Set<CelebrityProfileRow>();



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Keyless entity mapping for CelebritySummaryRow
            modelBuilder.Entity<CelebritySummaryRow>(eb =>
            {
                eb.HasNoKey();
                eb.ToView(null); // it's from a function, not a table/view

                eb.Property(p => p.Nconst).HasColumnName("nconst");
                eb.Property(p => p.PrimaryName).HasColumnName("primaryname");
                eb.Property(p => p.Photo_Url).HasColumnName("photo_url");
                eb.Property(p => p.Age).HasColumnName("age");
                eb.Property(p => p.Weighted_Avg).HasColumnName("weighted_avg");
            });

            // Keyless entity mapping for CelebrityProfileRow
            modelBuilder.Entity<CelebrityProfileRow>(eb =>
            {
                eb.HasNoKey();
                eb.ToView(null);

                eb.Property(p => p.Nconst).HasColumnName("nconst");
                eb.Property(p => p.PrimaryName).HasColumnName("primaryname");
                eb.Property(p => p.BirthYear).HasColumnName("birthyear");
                eb.Property(p => p.DeathYear).HasColumnName("deathyear");
                eb.Property(p => p.Professions).HasColumnName("professions");
                eb.Property(p => p.Weighted_Avg).HasColumnName("weighted_avg");
                eb.Property(p => p.KnownFor).HasColumnName("known_for");
                eb.Property(p => p.CreditsCount).HasColumnName("credits_count");
                eb.Property(p => p.AggregatedAwards).HasColumnName("aggregated_awards");
                eb.Property(p => p.PhotoUrl).HasColumnName("photo_url");
                eb.Property(p => p.Bio).HasColumnName("bio");
                eb.Property(p => p.Total_Votes).HasColumnName("total_votes");
            });
        }
    }
}

