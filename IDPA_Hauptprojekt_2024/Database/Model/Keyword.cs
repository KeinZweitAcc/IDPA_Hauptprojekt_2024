using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPA_Hauptprojekt_2024.Database.Model
{
    public class Keyword
    {
        public int Id { get; set; }
        public string Keywords { get; set; } = string.Empty;

        //Many-To-Many Navigation property
        public ICollection<ArticleKeyword> Article_Keywords { get; set; }
    }
}
