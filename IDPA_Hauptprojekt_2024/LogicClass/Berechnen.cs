using System;

namespace IDPA_Hauptprojekt_2024.LogicClass
{
    public class Berechnen
    {
        public string KuendigungsfristBerrechnen(DateTime? eintrittsdatum, DateTime? kuendigungsdatum)
        {
            TimeSpan? unterschied = kuendigungsdatum - eintrittsdatum;

            if (unterschied == null) return "Kündigungsfrist nicht berechenbar";
            if (kuendigungsdatum.Value < eintrittsdatum.Value) return "Das Kündigungsdatum kann nicht vor dem Eintrittsdatum liegen!";


            switch (unterschied?.Days)
            {
                case <= 365:
                    return "Kündigungsfrist beträgt 1 Monat";
                case > 365 and < 3285:
                    return "Kündigungsfrist beträgt 2 Monate";
                case > 3285:
                    return "Kündigungsfrist beträgt 3 Monate";
                default:
                    return "Kündigungsfrist nicht berechenbar";
            }
        }

        public string LohnfortzahlungBerrechnen(DateTime? eintrittsdatum, DateTime? kuendigungsdatum, string skala)
        {
            TimeSpan? unterschied = kuendigungsdatum - eintrittsdatum;

            if (unterschied == null) return "Lohnfortzahlung nicht berechenbar";
            if (kuendigungsdatum.Value < eintrittsdatum.Value) return "Das Erkankungs-/Unfallsdatum kann nicht vor dem Eintrittsdatum liegen!";

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
                <= 365 => "Lohnfortzahlung beträgt 3 Wochen",
                > 365 and <= 730 => "Lohnfortzahlung beträgt 1 Monat",
                > 730 and <= 1460 => "Lohnfortzahlung beträgt 2 Monate",
                > 1460 and <= 3285 => "Lohnfortzahlung beträgt 3 Monate",
                > 3285 and <= 5110 => "Lohnfortzahlung beträgt 4 Monate",
                > 5110 and <= 6935 => "Lohnfortzahlung beträgt 5 Monate",
                > 6935 => "Lohnfortzahlung beträgt 6 Monate",
                
            };
        }

        private string BaselLohnfortzahlung(int days)
        {
            return days switch
            {
                <= 365 => "Lohnfortzahlung beträgt 3 Wochen",
                > 365 and <= 1095 => "Lohnfortzahlung beträgt 2 Monate",
                > 1095 and <= 3650 => "Lohnfortzahlung beträgt 3 Monate",
                > 3650 => "Lohnfortzahlung beträgt 4 Monate",
               
            };
        }

        private string ZurichLohnfortzahlung(int days)
        {
            return days switch
            {
                <= 365 => "Lohnfortzahlung beträgt 3 Wochen",
                > 365 and <= 730 => "Lohnfortzahlung beträgt 8 Wochen",
                > 730 and <= 1095 => "Lohnfortzahlung beträgt 9 Wochen",
                > 1095 and <= 1460 => "Lohnfortzahlung beträgt 10 Wochen",
                > 1460 and <= 1825 => "Lohnfortzahlung beträgt 11 Wochen",
                > 1825 and <= 2190 => "Lohnfortzahlung beträgt 12 Wochen",
                > 2190 and <= 2555 => "Lohnfortzahlung beträgt 13 Wochen",
                > 2555 and <= 2920 => "Lohnfortzahlung beträgt 14 Wochen",
                > 2920 and <= 3285 => "Lohnfortzahlung beträgt 15 Wochen",
                > 3285 and <= 3650 => "Lohnfortzahlung beträgt 16 Wochen",
                > 3650 and <= 4015 => "Lohnfortzahlung beträgt 17 Wochen",
                _ => "Lohnfortzahlung nicht berechenbar"
            };
        }
    }
}
