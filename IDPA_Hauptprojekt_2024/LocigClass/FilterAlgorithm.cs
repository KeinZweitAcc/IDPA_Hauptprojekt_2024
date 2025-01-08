using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using IDPA_Hauptprojekt_2024.Database;
using IDPA_Hauptprojekt_2024.Database.Model;
using Microsoft.EntityFrameworkCore;

namespace IDPA_Hauptprojekt_2024.LogicClass
{
    public class FilterAlgorithm
    {
        private readonly DataContext _dataConnection;

        public FilterAlgorithm(DataContext dataContext)
        {
            _dataConnection = dataContext;
        }

        public List<Articles> Filter(string text)
        {
            // Wählt die Sektion Keywords aus der Datenbank aus und speichert sie in einer Liste.
            List<string> keyWords = _dataConnection.Keywords
                                                   .Select(x => x.Keyword)
                                                   .ToList();

            // Filtert die Keywords aus der Liste heraus, die im Text enthalten sind.
            var filteredWords = keyWords
                .Where(keyword => text.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return FilterArticles(filteredWords);
        }

        public List<Articles> FilterArticles(List<string> keywords)
        {
            // Wählt die Artikel aus der Datenbank aus, die die Keywords enthalten.
            List<int> articleIds = _dataConnection.ArticleKeywords
                                                  .Where(ak => keywords.Contains(ak.Keyword.Keyword))
                                                  .Select(ak => ak.ArticleId)
                                                  .Distinct()
                                                  .ToList();

            List<Articles> articles = _dataConnection.Articles
                                                     .Where(article => articleIds.Contains(article.Id))
                                                     .ToList();

            return articles;
        }
    }
}