using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Title> Titles { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Title>(entity =>
    {
        entity.ToTable("title_basics");
        entity.HasKey(e => e.Tconst);
        entity.Property(e => e.Tconst).HasColumnName("tconst");
        entity.Property(e => e.TitleType).HasColumnName("titletype");
        entity.Property(e => e.PrimaryTitle).HasColumnName("primarytitle");
        entity.Property(e => e.OriginalTitle).HasColumnName("originaltitle");
        entity.Property(e => e.IsAdult).HasColumnName("isadult");
        entity.Property(e => e.StartYear).HasColumnName("startyear");
        entity.Property(e => e.EndYear).HasColumnName("endyear");
        entity.Property(e => e.RuntimeMinutes).HasColumnName("runtimeminutes");
        entity.Property(e => e.Awards).HasColumnName("awards");
        entity.Property(e => e.Plot).HasColumnName("plot");
        entity.Property(e => e.Rated).HasColumnName("rated");
        entity.Property(e => e.Released).HasColumnName("released");
        entity.Property(e => e.Poster).HasColumnName("poster");
        entity.Property(e => e.ImdbVotes).HasColumnName("imdbvotes");
        entity.Property(e => e.BoxOffice).HasColumnName("boxoffice");
        entity.Property(e => e.Website).HasColumnName("website");
        entity.Property(e => e.Metascore).HasColumnName("metascore");
    });
}

    }
}
