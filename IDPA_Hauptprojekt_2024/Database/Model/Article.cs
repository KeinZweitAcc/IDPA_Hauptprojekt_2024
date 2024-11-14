using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPA_Hauptprojekt_2024.Database.Model
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        public string ArticleLaw { get; set; } = string.Empty;

        //Many-To-Many Navigation property
        public ICollection<ArticleKeyword> Article_Keywords { get; set; }
    }
}
