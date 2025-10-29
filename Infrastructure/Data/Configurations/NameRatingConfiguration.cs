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
    public class NameRatingConfiguration : IEntityTypeConfiguration<NameRating>
    {
        public void Configure(EntityTypeBuilder<NameRating> entity)
        {
            entity.ToTable("name_ratings");
            entity.HasKey(e => e.Nconst);
        }
    }
}
