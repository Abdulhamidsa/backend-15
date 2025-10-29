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
    public class TitleGenreConfiguration : IEntityTypeConfiguration<TitleGenre>
    {
        public void Configure(EntityTypeBuilder<TitleGenre> entity)
        {
            entity.ToTable("title_genre");
            entity.HasKey(e => new { e.Tconst, e.GenreId });
        }
    }
}
