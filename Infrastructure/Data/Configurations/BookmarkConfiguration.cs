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
    public class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
    {
        public void Configure(EntityTypeBuilder<Bookmark> entity)
        {
            entity.ToTable("bookmarks");
            entity.HasKey(e => e.BookmarkId);
            entity.HasIndex(e => new { e.UserId, e.Tconst }).IsUnique();
        }
    }
}
