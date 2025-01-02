using System.IO;
using System.Text;
using System.Windows;
using IDPA_Hauptprojekt_2024.Database;
using IDPA_Hauptprojekt_2024.Database.Model;
using Microsoft.EntityFrameworkCore;

public class DataImporter
{
    private readonly DataContext _context;

    public DataImporter(DataContext context)
    {
        _context = context;
    }

    public void ImportData()
    {
        const string keywordsPath = "Database/Data/keywords.csv";
        const string articlesPath = "Database/Data/articles.csv";
        const string articleKeywordsPath = "Database/Data/jointable_keyword_articles.csv";

        var keywords = LoadKeywordsFromCsv(keywordsPath);
        var articles = LoadArticlesFromCsv(articlesPath);
        var articleKeywords = LoadArticleKeywordsFromCsv(articleKeywordsPath);

        SaveDataToDatabase(keywords, articles, articleKeywords);
    }

    private List<Keywords> LoadKeywordsFromCsv(string filePath)
    {
        var keywords = new List<Keywords>();

        //  Encoding.GetEncoding("ISO-8859-1")) is used to display german "umlauts" äöü correctly.

        using (var reader = new StreamReader(filePath, Encoding.GetEncoding("ISO-8859-1")))
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
                        // You can manually set the ID here if necessary
                        Keyword = values[1].Trim()
                    });
                }
            }
        }

        return keywords;
    }

    private List<ArticleKeyword> LoadArticleKeywordsFromCsv(string filePath)
    {
        var articleKeywords = new List<ArticleKeyword>();

        using (var reader = new StreamReader(filePath, Encoding.GetEncoding("ISO-8859-1")))
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

                var idsArrayInt = values[1].Split(',').Select(int.Parse).ToList();

                if (values.Length >= 2)
                {
                    idsArrayInt.ForEach(id =>
                    {
                        articleKeywords.Add(new ArticleKeyword
                        {
                            ArticleId = int.TryParse(values[0], out var articleId) ? articleId : 0,
                            KeywordId = id
                        });
                    });

                    /* articleKeywords.Add(new ArticleKeyword
                    {
                        ArticleId = int.TryParse(values[0], out var articleId) ? articleId : 0,
                        KeywordId = int.TryParse(values[1], out var keywordId) ? keywordId : 0
                    });*/
                }
            }
        }

        return articleKeywords;
    }

    private List<Articles> LoadArticlesFromCsv(string filePath)
    {
        var articles = new List<Articles>();

        using (var reader = new StreamReader(filePath, Encoding.GetEncoding("ISO-8859-1")))
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
                        Id = ParseInt(values[0]), // Ensure you're not using auto-increment here
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

    private int ParseInt(string value) => int.TryParse(value.Trim(), out var result) ? result : 0;
    private char ParseLetter(string value) => string.IsNullOrEmpty(value.Trim()) ? '\0' : value.Trim()[0];


    private async Task SaveDataToDatabase(IEnumerable<Keywords> keywords, IEnumerable<Articles> articles,
                                          IEnumerable<ArticleKeyword> articleKeywords)
    {
        // Add Keywords if not already in the database
        foreach (var keyword in keywords)
        {
            if (!await _context.Keywords.AnyAsync(k => k.Id == keyword.Id))
            {
                _context.Keywords.Add(keyword);
            }
            else
            {
                // Optionally, update the existing record
                var existingKeyword = await _context.Keywords.FirstAsync(k => k.Id == keyword.Id);
                existingKeyword.Keyword = keyword.Keyword;
            }
        }


        // Add Articles if not already in the database
        foreach (var article in articles)
        {
            if (!await _context.Articles.AnyAsync(a => a.Id == article.Id))
            {
                await _context.Articles.AddAsync(article);
            }
            else
            {
                // Optionally, update the existing record
                var existingArticle = await _context.Articles.FirstAsync(a => a.Id == article.Id);
                existingArticle.ArticleNr = article.ArticleNr;
                existingArticle.Paragraph = article.Paragraph;
                existingArticle.Letter = article.Letter;
                existingArticle.Subsection = article.Subsection;
                existingArticle.ArticleDescription = article.ArticleDescription;
            }
        }

        await _context.SaveChangesAsync(); // Save changes after keywords and articles are added
        _context.ChangeTracker.Clear();


        /*
        var existingKeywords = _context.Keywords.AsNoTracking().ToList();
        var existingArticles = _context.Articles.AsNoTracking().ToList();

        // Add ArticleKeywords after saving Articles and Keywords
        foreach (var articleKeyword in articleKeywords)
        {
            var keyword = existingKeywords.FirstOrDefault(k => k.Id == articleKeyword.KeywordId);
            var article = existingArticles.FirstOrDefault(a => a.Id == articleKeyword.ArticleId);

            if (!_context.ArticleKeywords.Local.Any(
                ak => ak.ArticleId == articleKeyword.ArticleId && ak.KeywordId == articleKeyword.KeywordId) &&
                !_context.ArticleKeywords.Any(
                    ak => ak.ArticleId == articleKeyword.ArticleId && ak.KeywordId == articleKeyword.KeywordId))
            {
                _context.ArticleKeywords.Add(articleKeyword);
            }
        }*/

        // Load existing Keywords and Articles from the database to ensure they are in the context
        var existingKeywords = await _context.Keywords.AsNoTracking().ToListAsync();
        var existingArticles = await _context.Articles.AsNoTracking().ToListAsync();

        foreach (var articleKeyword in articleKeywords)
        {
            var keywordExists = existingKeywords.Any(k => k.Id == articleKeyword.KeywordId);
            var articleExists = existingArticles.Any(a => a.Id == articleKeyword.ArticleId);

            if (keywordExists && articleExists)
            {
                if (!_context.ArticleKeywords.Local.Any(
                    ak => ak.ArticleId == articleKeyword.ArticleId && ak.KeywordId == articleKeyword.KeywordId) &&
                    !await _context.ArticleKeywords.AnyAsync(
                        ak => ak.ArticleId == articleKeyword.ArticleId && ak.KeywordId == articleKeyword.KeywordId))
                {
                    await _context.ArticleKeywords.AddAsync(articleKeyword);
                }
            }
        }

        await _context.SaveChangesAsync();

        _context.ChangeTracker.Clear();
    }
}