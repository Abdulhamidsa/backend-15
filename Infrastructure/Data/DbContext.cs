using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Title> Titles { get; set; }
        public DbSet<Bookmarks> Bookmarks { get; set; }
        public DbSet<Genre> Genre { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            //Bookmarks
            modelBuilder.Entity<Bookmark>(e =>
            {
            e.HasOne(b => b.User).WithMany(u => u.Bookmarks).HasForeignKey(b => b.UserId);
            e.HasOne(b => b.Title).WithMany(t => t.Bookmarks).HasForeignKey(b => b.Tconst);
             
            });

            //Genre

            modelBuilder.Entity<Genre>(e =>
            {
                e.ToTable("genre");
                e.HasKey(g => g.GenreId);
                e.Property(g => g.Name).HasMaxLength(100);

            });

            //NameKnownForTitles

            modelBuilder.Entity<NameKnownForTitle>(e =>
            {
                e.HasKey(nk => new { nk.Nconst, nk.Tconst });
                e.HasOne(nk => nk.Name).WithMany(n => n.KnownForTitles).HasForeignKey(nk => nk.Nconst);
                e.HasOne(nk => nk.Title).WithMany(t => t.KnownByNames).HasForeignKey(nk => nk.Tconst);

            });

            //NameProfession

            modelBuilder.Entity<NameProfession>(e =>
            {
                e.HasKey(np => new { np.Nconst, np.ProfessionId });
                e.HasOne(np => np.Name).WithMany(n => n.Professions).HasForeignKey(np => np.Nconst);
                e.HasOne(np => np.Profession).WithMany(p => p.People).HasForeignKey(np => np.ProfessionId);
            });

            //TitleAkas

            modelBuilder.Entity<TitleAkas>(e =>
            {
                e.HasKey(t => new { t.TitleId, t.Ordering }); // Composite key
                e.HasOne(t => t.TitleBasics).WithMany(tb => tb.AlternateTitles).HasForeignKey(t => t.TitleId);
            });






        }
    }
}