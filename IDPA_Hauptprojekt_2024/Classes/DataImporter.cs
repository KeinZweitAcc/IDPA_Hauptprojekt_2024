using IDPA_Hauptprojekt_2024.Database.Model;
using IDPA_Hauptprojekt_2024.Database;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Diagnostics;
using System.Windows;


public class DataImporter
{
    private readonly DataContext _context;

    public DataImporter(DataContext context)
    {
        _context = context;
    }

    public void ImportData()
    {
        const string articleKeywordJointableKeywordPath = "Database/Data/jointable_keyword_articles.csv";
        const string keywordsPath = "Database/Data/keywords.csv";
        const string articlesPath = "Database/Data/articles.csv";

        var keywords = LoadKeywordsFromCsv(keywordsPath);
        var articles = LoadArticlesFromCsv(articlesPath);
        var keywordArticleRelations = LoadKeywordArticleRelations(articleKeywordJointableKeywordPath);

        SaveDataToDatabase(keywords, articles, keywordArticleRelations);
    }

    private List<Keywords> LoadKeywordsFromCsv(string filePath)
    {
        var keywords = new List<Keywords>();

        using (var reader = new StreamReader(filePath))
        {
            bool isFirstLine = true;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (isFirstLine)
                {
                    isFirstLine = false; // Skip header
                    continue;
                }

                var values = line.Split(";;");

                if (values.Length >= 2)
                {
                    keywords.Add(new Keywords
                    {
                        Id = int.TryParse(values[0], out var id) ? id : 0,
                        Keyword = values[1].Trim()
                    });
                }
            }
        }

        return keywords;
    }

    private List<Articles> LoadArticlesFromCsv(string filePath)
    {
        var articles = new List<Articles>();

        using (var reader = new StreamReader(filePath))
        {
            bool isFirstLine = true;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (isFirstLine)
                {
                    isFirstLine = false; // Skip header
                    continue;
                }

                var values = line.Split(new[] { ";;" }, StringSplitOptions.None);

                if (values.Length >= 6)
                {
                    articles.Add(new Articles
                    {
                        Id = ParseInt(values[0]),
                        ArticleNr = values[1].Trim(),
                        Paragraph = ParseInt(values[2]),
                        Letter = ParseLetter(values[3]),
                        Subsection = ParseInt(values[4]),
                        ArticleDescription = values[5].Trim('"')
                    });
                }
            }
        }

        return articles;
    }

    private List<ArticleKeyword> LoadKeywordArticleRelations(string filePath)
    {
        var keywordArticles = new List<ArticleKeyword>();

        using (var reader = new StreamReader(filePath))
        {
            bool isFirstLine = true;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (isFirstLine)
                {
                    isFirstLine = false; // Skip header
                    continue;
                }

                var values = line.Split(";;");

                if (values.Length >= 2)
                {
                    var keywordId = int.TryParse(values[0], out var id) ? id : 0;
                    var articleIds = values[1].Split(',')
                                              .Select(id => int.TryParse(id.Trim(), out var articleId) ? articleId : 0)
                                              .Where(articleId => articleId > 0);

                    foreach (var articleId in articleIds)
                    {
                        keywordArticles.Add(new ArticleKeyword
                        {
                            KeywordId = keywordId,
                            ArticleId = articleId
                        });
                    }
                }
            }
        }

        Console.WriteLine("Relations loaded successfully.");
        return keywordArticles;
    }

    private int ParseInt(string value) => int.TryParse(value.Trim(), out var result) ? result : 0;
    private char ParseLetter(string value) => string.IsNullOrEmpty(value.Trim()) ? '\0' : value.Trim()[0];

    private void SaveDataToDatabase(IEnumerable<Keywords> keywords, IEnumerable<Articles> articles,
                                    IEnumerable<ArticleKeyword> keywordArticleRelations)
    {
        // Lade existierende Artikel und Stichwörter
        var existingArticleIds = _context.Articles.Select(a => a.Id).ToHashSet();
        var existingKeywordIds = _context.Keywords.Select(k => k.Id).ToHashSet();

        // Nur neue Artikel und Stichwörter hinzufügen
        var newArticles = articles.Where(a => !existingArticleIds.Contains(a.Id)).ToList();
        var newKeywords = keywords.Where(k => !existingKeywordIds.Contains(k.Id)).ToList();

        _context.Articles.AddRange(newArticles);
        _context.Keywords.AddRange(newKeywords);

        // Lade existierende Relationen aus der Datenbank und lokalem Tracking
        var existingRelations = _context.ArticleKeywords
                                        .Select(ak => new { ak.ArticleId, ak.KeywordId })
                                        .ToHashSet();

        foreach (var relation in keywordArticleRelations)
        {
            // Verhindere das Hinzufügen von doppelten Relationen
            if (!existingRelations.Contains(new { relation.ArticleId, relation.KeywordId }))
            {
                // Prüfen, ob bereits eine Instanz lokal verfolgt wird
                if (
                    !_context.ArticleKeywords.Local.Any(
                        ak => ak.ArticleId == relation.ArticleId && ak.KeywordId == relation.KeywordId))
                {
                    _context.ArticleKeywords.Add(relation);
                }
            }
        }

        // Änderungen speichern
        _context.SaveChanges();

        Console.WriteLine("Data imported successfully.");
    }
}
