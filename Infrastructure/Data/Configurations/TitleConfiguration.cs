using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class TitleConfiguration : IEntityTypeConfiguration<Title>
    {
        public void Configure(EntityTypeBuilder<Title> entity)
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

            // Relationships
            entity.HasMany(e => e.AlternateTitles)
                  .WithOne(a => a.TitleRef)
                  .HasForeignKey(a => a.TitleId);

            entity.HasMany(e => e.TitleGenres)
                  .WithOne(g => g.Title)
                  .HasForeignKey(g => g.Tconst);

            entity.HasMany(e => e.Directors)
                  .WithOne(d => d.Title)
                  .HasForeignKey(d => d.Tconst);

            entity.HasMany(e => e.Writers)
                  .WithOne(w => w.Title)
                  .HasForeignKey(w => w.Tconst);

            entity.HasMany(e => e.Principals)
                  .WithOne(p => p.Title)
                  .HasForeignKey(p => p.Tconst);

            entity.HasMany(e => e.Characters)
                  .WithOne(c => c.Title)
                  .HasForeignKey(c => c.Tconst);

            entity.HasMany(e => e.Episodes)
                  .WithOne(e => e.ParentTitle)
                  .HasForeignKey(e => e.ParentTconst)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
