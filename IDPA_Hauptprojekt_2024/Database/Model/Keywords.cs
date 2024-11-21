using System.ComponentModel.DataAnnotations;

namespace IDPA_Hauptprojekt_2024.Database.Model
{
    public class Keywords
    {
        [Key]
        public int Id { get; set; }

        public required string Keyword { get; set; }

        //Many-To-Many Navigation property
        public ICollection<ArticleKeyword> ArticleKeywords { get; set; }
    }
}
