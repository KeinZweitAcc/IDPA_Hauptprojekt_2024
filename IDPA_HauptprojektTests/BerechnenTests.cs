using IDPA_Hauptprojekt_2024.LogicClass;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IDPA_HauptprojektTests
{
    [TestClass]
    public class UnitTest1
    {
        private Berechnen _berechnungen;

        [TestInitialize]
        public void Initialize()
        {
            _berechnungen = new Berechnen();
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

            Assert.AreEqual("2 Monate", result);
        }

        [TestMethod]
        public void TestLohnfortzahlungBerrechnen_Zuerich()
        {
            DateTime eintrittsdatum = new DateTime(2020, 1, 1);
            DateTime erkrankungsdatum = new DateTime(2023, 1, 1);
            string skala = "Zürich";

            string result = _berechnungen.LohnfortzahlungBerrechnen(eintrittsdatum, erkrankungsdatum, skala);

            Assert.AreEqual("10 Wochen", result);
        }
    }
}
