using IDPA_Hauptprojekt_2024.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace IDPA_Hauptprojekt_2024.Database
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Articles> Articles { get; set; }
        public DbSet<Keywords> Keywords { get; set; }
        public DbSet<ArticleKeyword> ArticleKeywords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleKeyword>()
                .HasKey(mab => new { mab.ArticleId, mab.KeywordId });

            modelBuilder.Entity<ArticleKeyword>()
                .HasOne(mab => mab.Article)
                .WithMany(a => a.ArticleKeywords)
                .HasForeignKey(a => a.ArticleId)
                .OnDelete(DeleteBehavior.Cascade); //Deletes the relation as well.


            modelBuilder.Entity<ArticleKeyword>()
                .HasOne(mab => mab.Keyword)
                .WithMany(a => a.ArticleKeywords)
                .HasForeignKey(mab => mab.KeywordId)
                .OnDelete(DeleteBehavior.Cascade); //Deletes the relation as well.
        }
    }
}
