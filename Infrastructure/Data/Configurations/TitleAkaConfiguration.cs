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
    public class TitleAkaConfiguration : IEntityTypeConfiguration<TitleAka>
    {
        public void Configure(EntityTypeBuilder<TitleAka> entity)
        {
            entity.ToTable("title_akas");
            entity.HasKey(e => new { e.TitleId, e.Ordering });
        }
    }
}
