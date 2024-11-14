using IDPA_Hauptprojekt_2024.Database.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPA_Hauptprojekt_2024.Database
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<ArticleKeyword> Article_Keywords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleKeyword>()
                .HasKey(mab => new { mab.ArticleId, mab.KeywordId });

            modelBuilder.Entity<ArticleKeyword>()
                .HasOne(mab => mab.Article)
                .WithMany(a => a.Article_Keywords)
                .HasForeignKey(a => a.ArticleId)
                .OnDelete(DeleteBehavior.Cascade); //Deletes the relation as well.


            modelBuilder.Entity<ArticleKeyword>()
                .HasOne(mab => mab.Keyword)
                .WithMany(a => a.Article_Keywords)
                .HasForeignKey(mab => mab.KeywordId)
                .OnDelete(DeleteBehavior.Cascade); //Deletes the relation as well.
        }
    }
}
