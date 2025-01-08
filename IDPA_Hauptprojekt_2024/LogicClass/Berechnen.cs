using System;

namespace IDPA_Hauptprojekt_2024.LogicClass
{
    public class Berechnen
    {
        public string KuendigungsfristBerrechnen(DateTime? eintrittsdatum, DateTime? kuendigungsdatum)
        {
            TimeSpan? unterschied = kuendigungsdatum - eintrittsdatum;

            if (unterschied == null) return "Kuendigungsfrist nicht berechenbar";

            switch (unterschied?.Days)
            {
                case <= 365:
                    return "1 Monat";
                case > 365 and < 3285:
                    return "2 Monate";
                case > 3285:
                    return "3 Monate";
                default:
                    return "Kuendigungsfrist nicht berechenbar";
            }
        }

        public string LohnfortzahlungBerrechnen(DateTime? eintrittsdatum, DateTime? kuendigungsdatum, string skala)
        {
            TimeSpan? unterschied = kuendigungsdatum - eintrittsdatum;

            if (unterschied == null) return "Lohnfortzahlung nicht berechenbar";

            if (skala == "Bern")
            {
                return BernLohnfortzahlung(unterschied.Value.Days);
            }
            else if (skala == "Basel")
            {
                return BaselLohnfortzahlung(unterschied.Value.Days);
            }
            else if (skala == "Zürich")
            {
                return ZurichLohnfortzahlung(unterschied.Value.Days);
            }

            return "Lohnfortzahlung nicht berechenbar";
        }

        private string BernLohnfortzahlung(int days)
        {
            return days switch
            {
                <= 365 => "3 Wochen",
                > 365 and <= 730 => "1 Monat",
                > 730 and <= 1460 => "2 Monate",
                > 1460 and <= 3285 => "3 Monate",
                > 3285 and <= 5110 => "4 Monate",
                > 5110 and <= 6935 => "5 Monate",
                > 6935 => "6 Monate",
                
            };
        }

        private string BaselLohnfortzahlung(int days)
        {
            return days switch
            {
                <= 365 => "3 Wochen",
                > 365 and <= 1095 => "2 Monate",
                > 1095 and <= 3650 => "3 Monate",
                > 3650 => "4 Monate",
               
            };
        }

        private string ZurichLohnfortzahlung(int days)
        {
            return days switch
            {
                <= 365 => "3 Wochen",
                > 365 and <= 730 => "8 Wochen",
                > 730 and <= 1095 => "9 Wochen",
                > 1095 and <= 1460 => "10 Wochen",
                > 1460 and <= 1825 => "11 Wochen",
                > 1825 and <= 2190 => "12 Wochen",
                > 2190 and <= 2555 => "13 Wochen",
                > 2555 and <= 2920 => "14 Wochen",
                > 2920 and <= 3285 => "15 Wochen",
                > 3285 and <= 3650 => "16 Wochen",
                > 3650 and <= 4015 => "17 Wochen",
                _ => "Lohnfortzahlung nicht berechenbar"
            };
        }
    }
}
