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
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> entity)
        {
            entity.ToTable("genre");
            entity.HasKey(e => e.GenreId);
            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.Name).HasColumnName("name");
        }
    }
}
