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
    public class RatingHistoryConfiguration : IEntityTypeConfiguration<RatingHistory>
    {
        public void Configure(EntityTypeBuilder<RatingHistory> entity)
        {
            entity.ToTable("rating_history");
            entity.HasKey(e => new { e.UserId, e.Tconst });
        }
    }
}
