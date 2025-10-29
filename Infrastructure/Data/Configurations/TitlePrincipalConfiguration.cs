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
    public class TitlePrincipalConfiguration : IEntityTypeConfiguration<TitlePrincipal>
    {
        public void Configure(EntityTypeBuilder<TitlePrincipal> entity)
        {
            entity.ToTable("title_principals");
            entity.HasKey(e => new { e.Tconst, e.Ordering });
        }
   
    }
}
