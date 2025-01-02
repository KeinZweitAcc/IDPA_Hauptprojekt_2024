using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IDPA_Hauptprojekt_2024.Database.Model
{
    public class ArticleKeyword
    {
        [Key]
        public int ArticleId { get; set; }

        [ForeignKey(nameof(ArticleId))]
        public Articles Article { get; set; }

        [Key]
        public int KeywordId { get; set; }

        [ForeignKey(nameof(KeywordId))]
        public Keywords Keyword { get; set; }
    }
}