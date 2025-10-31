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
            // entity.ToTable("bookmarks");
            // entity.Property(e => e.BookmarkId).HasColumnName("bookmark_id").IsRequired();
            // entity.Property(e => e.UserId).HasColumnName("user_id").IsRequired();
            // entity.Property(e => e.Tconst).HasColumnName("tconst").IsRequired();}



            // Table
            entity.ToTable("bookmarks");

            // Primary key
            entity.HasKey(e => e.BookmarkId);

            // Column mappings
            entity.Property(e => e.BookmarkId)
                  .HasColumnName("bookmark_id");

            entity.Property(e => e.UserId)
                  .HasColumnName("user_id");

            entity.Property(e => e.Tconst)
                  .HasColumnName("tconst");

            entity.Property(e => e.CreatedAt)
                  .HasColumnName("created_at");

            // Unique(user_id, tconst)
            entity.HasIndex(e => new { e.UserId, e.Tconst })
                  .IsUnique();




            // Relationships

            // Bookmark → User (many bookmarks per user)
            entity.HasOne(e => e.User)
                  .WithMany(u => u.Bookmarks)
                  .HasForeignKey(e => e.UserId)
                  .HasPrincipalKey(u => u.Id)
                  .OnDelete(DeleteBehavior.Cascade);

            // Bookmark → Title (many bookmarks per title)
            entity.HasOne(e => e.Title)
                  .WithMany(t => t.Bookmarks)
                  .HasForeignKey(e => e.Tconst)
                  .HasPrincipalKey(t => t.Tconst)
                  .OnDelete(DeleteBehavior.Cascade);



        }


    }
}
