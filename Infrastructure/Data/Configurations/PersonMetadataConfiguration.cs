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
    public class PersonMetadataConfiguration : IEntityTypeConfiguration<PersonMetadata>
    {
        public void Configure(EntityTypeBuilder<PersonMetadata> entity)
        {
            entity.ToTable("person_metadata");

            // Primary key
            entity.HasKey(pm => pm.Nconst);

            // Columns
            entity.Property(pm => pm.Nconst)
                  .HasColumnName("nconst");

            entity.Property(pm => pm.PhotoUrl)
                  .HasColumnName("photo_url");

            entity.Property(pm => pm.Bio)
                  .HasColumnName("bio");

            // 1-to-1 relationship: person_metadata (child) → name_basics (parent)
            entity.HasOne(pm => pm.Person)
                  .WithOne(n => n.Metadata)
                  .HasForeignKey<PersonMetadata>(pm => pm.Nconst)
                  .HasPrincipalKey<Name>(n => n.Nconst)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
    

