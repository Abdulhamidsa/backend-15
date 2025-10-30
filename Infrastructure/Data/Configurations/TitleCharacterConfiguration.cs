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
    public class TitleCharacterConfiguration : IEntityTypeConfiguration<TitleCharacter>
    {
        public void Configure(EntityTypeBuilder<TitleCharacter> entity)
        {
            entity.ToTable("title_characters");

            // Composite key (tconst + nconst + character)
            entity.HasKey(e => new { e.Tconst, e.Nconst, e.Character });

            // Column mappings
            entity.Property(e => e.Tconst).HasColumnName("tconst");
            entity.Property(e => e.Nconst).HasColumnName("nconst");
            entity.Property(e => e.Character).HasColumnName("character");

            // Relationships
            entity.HasOne(e => e.Title)
                  .WithMany(t => t.Characters)
                  .HasForeignKey(e => e.Tconst)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Person)
                  .WithMany(n => n.Characters)
                  .HasForeignKey(e => e.Nconst)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
    
    }

