using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDPA_Hauptprojekt_2024.Database.Logic;
using Microsoft.EntityFrameworkCore.Internal;
using IDPA_Hauptprojekt_2024_Migration.Database;
using IDPA_Hauptprojekt_2024.Database;
using IDPA_Hauptprojekt_2024.Database.Model;
using System.Windows.Documents;

namespace IDPA_Hauptprojekt_2024.LocigClass
{
    public class FilterAlgorithm
    {
        private readonly DatabaseHandleClass _databaseHandleClass;
        private DataContext _dataConnection;

        public FilterAlgorithm(DatabaseHandleClass databaseHandleClass, DataContext dataContext)
        {
            _dataConnection = dataContext;
            _databaseHandleClass = databaseHandleClass;
        }

        public List<Article> Filter(string text)
        {
            // Wählt die Sektion Keywords aus der Datenbank aus und speichert sie in einer Liste.
            List<string> keyWords = _dataConnection.Keywords
                .Select(x => x.Keywords)
                .ToList();

            // Filtert die Keywords aus der Liste heraus, die im Text enthalten sind.
            var filteredWords = keyWords
                .Where(keyword => text.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return FilterArticles(filteredWords);
        }

        public List<Article> FilterArticles(List<string> keywords)
        {
            // Wählt die Artikel aus der Datenbank aus, die die Keywords enthalten.
            List<Article> articles = _dataConnection.Articles
                .Where(article => article.Article_Keywords.Any(articleKeyword => keywords.Contains(articleKeyword.Keyword.Keywords)))
                .ToList();

            return articles;
        }
    }
}