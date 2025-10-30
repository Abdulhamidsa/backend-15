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
    public class ProfessionConfiguration : IEntityTypeConfiguration<Profession>
    {
        public void Configure(EntityTypeBuilder<Profession> entity)
        {
            entity.ToTable("profession");
            entity.HasKey(e => e.ProfessionId);
            entity.Property(e => e.ProfessionId).HasColumnName("profession_id");
            entity.Property(e => e.Name).HasColumnName("name");
        }
    }
}
