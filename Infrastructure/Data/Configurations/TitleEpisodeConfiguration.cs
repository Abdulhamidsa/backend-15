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
    public class TitleEpisodeConfiguration : IEntityTypeConfiguration<TitleEpisode>
    {
        public void Configure(EntityTypeBuilder<TitleEpisode> entity)
        {
            entity.ToTable("title_episode");
            entity.HasKey(e => e.Tconst);
        }
    }
}
