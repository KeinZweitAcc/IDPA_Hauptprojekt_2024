using IDPA_Hauptprojekt_2024.LogicClass;


namespace IDPA_HauptprojektTests
{
    
        [TestClass]
        public class UnitTest1
        {
            private Mock<DataContext> _mockContext;
            private Mock<DbSet<Keywords>> _mockKeywordsSet;
            private Mock<DbSet<ArticleKeyword>> _mockArticleKeywordsSet;
            private Mock<DbSet<Articles>> _mockArticlesSet;
            private Berechnungen _berechnungen;

            [TestInitialize]
            public void Initialize()
            {
            _berechnungen = new Berechnungen(new MainWindow(null));
            _mockContext = new Mock<DataContext>();

                var keywords = new List<Keywords>
            {
                new Keywords { Id = 1, Keyword = "Test" },
                new Keywords { Id = 2, Keyword = "Example" }
                }.AsQueryable();

                _mockKeywordsSet = new Mock<DbSet<Keywords>>();
                _mockKeywordsSet.As<IQueryable<Keywords>>().Setup(m => m.Provider).Returns(keywords.Provider);
                _mockKeywordsSet.As<IQueryable<Keywords>>().Setup(m => m.Expression).Returns(keywords.Expression);
                _mockKeywordsSet.As<IQueryable<Keywords>>().Setup(m => m.ElementType).Returns(keywords.ElementType);
                _mockKeywordsSet.As<IQueryable<Keywords>>().Setup(m => m.GetEnumerator()).Returns(keywords.GetEnumerator());

                var articleKeywords = new List<ArticleKeyword>
            {
                new ArticleKeyword { ArticleId = 1, KeywordId = 1, Keyword = keywords.First(), Article = new Articles { Id = 1, ArticleNr = "A1", ArticleDescription = "Description1" } },
                new ArticleKeyword { ArticleId = 2, KeywordId = 2, Keyword = keywords.Last(), Article = new Articles { Id = 2, ArticleNr = "A2", ArticleDescription = "Description2" } }
                }.AsQueryable();

                _mockArticleKeywordsSet = new Mock<DbSet<ArticleKeyword>>();
                _mockArticleKeywordsSet.As<IQueryable<ArticleKeyword>>().Setup(m => m.Provider).Returns(articleKeywords.Provider);
                _mockArticleKeywordsSet.As<IQueryable<ArticleKeyword>>().Setup(m => m.Expression).Returns(articleKeywords.Expression);
                _mockArticleKeywordsSet.As<IQueryable<ArticleKeyword>>().Setup(m => m.ElementType).Returns(articleKeywords.ElementType);
                _mockArticleKeywordsSet.As<IQueryable<ArticleKeyword>>().Setup(m => m.GetEnumerator()).Returns(articleKeywords.GetEnumerator());

                var articles = new List<Articles>
            {
                new Articles { Id = 1, ArticleNr = "A1", ArticleDescription = "Description1" },
                new Articles { Id = 2, ArticleNr = "A2", ArticleDescription = "Description2" }
                }.AsQueryable();

                _mockArticlesSet = new Mock<DbSet<Articles>>();
                _mockArticlesSet.As<IQueryable<Articles>>().Setup(m => m.Provider).Returns(articles.Provider);
                _mockArticlesSet.As<IQueryable<Articles>>().Setup(m => m.Expression).Returns(articles.Expression);
                _mockArticlesSet.As<IQueryable<Articles>>().Setup(m => m.ElementType).Returns(articles.ElementType);
                _mockArticlesSet.As<IQueryable<Articles>>().Setup(m => m.GetEnumerator()).Returns(articles.GetEnumerator());

                _mockContext.Setup(c => c.Keywords).Returns(_mockKeywordsSet.Object);
                _mockContext.Setup(c => c.ArticleKeywords).Returns(_mockArticleKeywordsSet.Object);
                _mockContext.Setup(c => c.Articles).Returns(_mockArticlesSet.Object);
            }

            [TestMethod]
            public void TestFilter()
            {
                var filterAlgorithm = new FilterAlgorithm(_mockContext.Object);
                var result = filterAlgorithm.Filter("Test");

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual("A1", result[0].ArticleNr);
            }

            [TestMethod]
            public void TestFilterArticles()
            {
                var filterAlgorithm = new FilterAlgorithm(_mockContext.Object);
                var result = filterAlgorithm.FilterArticles(new List<string> { "Test" });

                Assert.AreEqual(1, result.Count);
                Assert.AreEqual("A1", result[0].ArticleNr);
            }

        

   
        [TestMethod]
        public void TestKuendigungsfristBerrechnen_LessThanOneYear()
        {
            DateTime eintrittsdatum = new DateTime(2022, 1, 1);
            DateTime kuendigungsdatum = new DateTime(2022, 12, 31);

            string result = _berechnungen.KuendigungsfristBerrechnen(eintrittsdatum, kuendigungsdatum);

            Assert.AreEqual("1 Monat", result);
        }

        [TestMethod]
        public void TestKuendigungsfristBerrechnen_BetweenOneAndNineYears()
        {
            DateTime eintrittsdatum = new DateTime(2015, 1, 1);
            DateTime kuendigungsdatum = new DateTime(2020, 1, 1);

            string result = _berechnungen.KuendigungsfristBerrechnen(eintrittsdatum, kuendigungsdatum);

            Assert.AreEqual("2 Monate", result);
        }

        [TestMethod]
        public void TestKuendigungsfristBerrechnen_MoreThanNineYears()
        {
            DateTime eintrittsdatum = new DateTime(2010, 1, 1);
            DateTime kuendigungsdatum = new DateTime(2020, 1, 1);

            string result = _berechnungen.KuendigungsfristBerrechnen(eintrittsdatum, kuendigungsdatum);

            Assert.AreEqual("3 Monate", result);
        }

        [TestMethod]
        public void TestLohnfortzahlungBerrechnen_Bern()
        {
            DateTime eintrittsdatum = new DateTime(2020, 1, 1);
            DateTime erkrankungsdatum = new DateTime(2021, 1, 1);
            string skala = "Bern";

            string result = _berechnungen.LohnfortzahlungBerrechnen(eintrittsdatum, erkrankungsdatum, skala);

            Assert.AreEqual("1 Monat", result);
        }

        [TestMethod]
        public void TestLohnfortzahlungBerrechnen_Basel()
        {
            DateTime eintrittsdatum = new DateTime(2020, 1, 1);
            DateTime erkrankungsdatum = new DateTime(2022, 1, 1);
            string skala = "Basel";

            string result = _berechnungen.LohnfortzahlungBerrechnen(eintrittsdatum, erkrankungsdatum, skala);

            Assert.AreEqual("2 Monat", result);
        }

        [TestMethod]
        public void TestLohnfortzahlungBerrechnen_Zuerich()
        {
            DateTime eintrittsdatum = new DateTime(2020, 1, 1);
            DateTime erkrankungsdatum = new DateTime(2023, 1, 1);
            string skala = "Zürich";

            string result = _berechnungen.LohnfortzahlungBerrechnen(eintrittsdatum, erkrankungsdatum, skala);

            Assert.AreEqual("9 Wochen", result);
        }
    }
}

