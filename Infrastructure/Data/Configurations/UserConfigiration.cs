using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.ToTable("users");
        entity.HasKey(u => u.Id);
        entity.Property(u => u.Id).HasColumnName("user_id");
        entity.Property(u => u.Email).HasColumnName("email").IsRequired();
        entity.Property(u => u.Username).HasColumnName("username").IsRequired();
        entity.Property(u => u.PasswordHash).HasColumnName("password_hash").IsRequired();
        entity.Property(u => u.CreatedAt).HasColumnName("created_at");

        // Uniques
        entity.HasIndex(u => u.Email).IsUnique();
        entity.HasIndex(u => u.Username).IsUnique();
    }
}
