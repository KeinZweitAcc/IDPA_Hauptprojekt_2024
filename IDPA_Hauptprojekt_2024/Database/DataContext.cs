using IDPA_Hauptprojekt_2024.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace IDPA_Hauptprojekt_2024.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Articles> Articles { get; set; }
        public DbSet<Keywords> Keywords { get; set; }
        public DbSet<ArticleKeyword> ArticleKeywords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguration des zusammengesetzten Schlüssels für ArticleKeyword
            modelBuilder.Entity<ArticleKeyword>()
                        .HasKey(ak => new { ak.ArticleId, ak.KeywordId });

            // Konfiguration der Many-To-Many-Beziehungen
            modelBuilder.Entity<ArticleKeyword>()
                        .HasOne(ak => ak.Article)
                        .WithMany(a => a.ArticleKeywords)
                        .HasForeignKey(ak => ak.ArticleId);

            modelBuilder.Entity<ArticleKeyword>()
                        .HasOne(ak => ak.Keyword)
                        .WithMany(k => k.ArticleKeywords)
                        .HasForeignKey(ak => ak.KeywordId);
        }
    }
}
