using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IDPA_Hauptprojekt_2024.Database.Model
{
    public class ArticleKeyword
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public int KeywordId { get; set; }
        public Keyword Keyword { get; set; }
    }
}
