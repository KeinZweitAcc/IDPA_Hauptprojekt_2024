using System.IO;
using IDPA_Hauptprojekt_2024.Database;
using IDPA_Hauptprojekt_2024.Database.Model;

public class DataImporter
{
    private readonly DataContext _context;

    public DataImporter(DataContext context)
    {
        _context = context;
    }

    public void ImportData()
    {
        const string articleKeywordJointableKeywordPath = "Database/Data/jointable_keyword_article.csv";
        const string keywordsPath = "Database/Data/keywords.csv";
        const string articlesPath = "Database/Data/articles.csv";

        var keywords = LoadKeywordsFromCsv(keywordsPath);
        var keywordArticleRelations = LoadKeywordArticleRelations(articleKeywordJointableKeywordPath);
        var articles = LoadArticlesFromCsv(articlesPath);

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
                    var articleIds = values[2].Split(',').Select(id => int.TryParse(id, out var aid) ? aid : 0);

                    foreach (var articleId in articleIds)
                    {
                        if (articleId > 0)
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
        }

        return keywordArticles;
    }

    private int ParseInt(string value) => int.TryParse(value.Trim(), out var result) ? result : 0;
    private char ParseLetter(string value) => string.IsNullOrEmpty(value.Trim()) ? '\0' : value.Trim()[0];

    private void SaveDataToDatabase(IEnumerable<Keywords> keywords, IEnumerable<Articles> articles, IEnumerable<ArticleKeyword> keywordArticleRelations)
    {
        _context.Articles.AddRange(articles);
        _context.Keywords.AddRange(keywords);
        _context.ArticleKeywords.AddRange(keywordArticleRelations);

        _context.SaveChanges();
    }
}
