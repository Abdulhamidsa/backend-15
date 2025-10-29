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
    public class NameProfessionConfiguration : IEntityTypeConfiguration<NameProfession>
    {
        public void Configure(EntityTypeBuilder<NameProfession> entity)
        {
            entity.ToTable("name_profession");
            entity.HasKey(e => new { e.Nconst, e.ProfessionId });
        }
    }
}
