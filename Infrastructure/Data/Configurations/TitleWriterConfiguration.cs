using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class TitleWriterConfiguration : IEntityTypeConfiguration<TitleWriter>
    {
       public void Configure(EntityTypeBuilder<TitleWriter> entity)
        {
            entity.ToTable("title_writers");
            entity.HasKey(e => new { e.Tconst, e.Nconst });
        } 
    }
}