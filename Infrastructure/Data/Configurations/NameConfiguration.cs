using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class NameConfiguration : IEntityTypeConfiguration<Name>
    {
        public void Configure(EntityTypeBuilder<Name> entity)
        {
            entity.ToTable("name_basics");
            entity.HasKey(e => e.Nconst);

            entity.Property(e => e.Nconst).HasColumnName("nconst");
            entity.Property(e => e.PrimaryName).HasColumnName("primaryname");
            entity.Property(e => e.BirthYear).HasColumnName("birthyear");
            entity.Property(e => e.DeathYear).HasColumnName("deathyear");

            // Relationships
            entity.HasMany(e => e.Professions)
                  .WithOne(np => np.Person)
                  .HasForeignKey(np => np.Nconst);

            entity.HasMany(e => e.Principals)
                  .WithOne(p => p.Person)
                  .HasForeignKey(p => p.Nconst);

            entity.HasMany(e => e.Characters)
                  .WithOne(c => c.Person)
                  .HasForeignKey(c => c.Nconst);

            entity.HasMany(e => e.DirectedTitles)
                  .WithOne(d => d.Director)
                  .HasForeignKey(d => d.Nconst);

            entity.HasMany(e => e.WrittenTitles)
                  .WithOne(w => w.Writer)
                  .HasForeignKey(w => w.Nconst);

            entity.HasMany(e => e.KnownForTitles)
                  .WithOne(k => k.Person)
                  .HasForeignKey(k => k.Nconst);

            entity.HasOne(e => e.NameRating)
                  .WithOne(r => r.Person)
                  .HasForeignKey<NameRating>(r => r.Nconst);
        }
    }
}
