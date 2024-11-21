using System.ComponentModel.DataAnnotations;

namespace IDPA_Hauptprojekt_2024.Database.Model
{
    public class Articles
    {
        [Key]
        public int Id { get; set; }

        public required string ArticleNr { get; set; }  //347a
        public int Paragraph { get; set; }              //1
        public char Letter { get; set; }                //d
        public int Subsection { get; set; }             //null
                                                        //==> Art. 347a 1d

        public required string ArticleDescription { get; set; }

        //Many-To-Many Navigation property
        public ICollection<ArticleKeyword> ArticleKeywords { get; set; }
    }
}
