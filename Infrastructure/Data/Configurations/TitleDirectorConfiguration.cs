using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class TitleDirectorConfiguration : IEntityTypeConfiguration<TitleDirector>
    {
       public void Configure(EntityTypeBuilder<TitleDirector> entity)
        {
            entity.ToTable("title_directors");
            entity.HasKey(e => new { e.Tconst, e.Nconst });
        } 
    }
}