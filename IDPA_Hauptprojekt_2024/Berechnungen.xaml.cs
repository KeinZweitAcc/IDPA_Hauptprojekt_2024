using System.Windows;
using System.Windows.Controls;
using static MaterialDesignThemes.Wpf.Theme;

namespace IDPA_Hauptprojekt_2024
{
    /// <summary>
    /// Interaction logic for Berechnungen.xaml
    /// </summary>
    public partial class Berechnungen : Page
    {
        private MainWindow _mainWindow;
        public Berechnungen(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();
            InitializeSkalen();
        }
        private string[] skalen = { "Bern", "Basel", "Zürich" };
        private void InitializeSkalen()
        {
            ComboBoxSkala_Lohnfortzahlung.Items.Clear();
            foreach (var skala in skalen)
            {
                ComboBoxSkala_Lohnfortzahlung.Items.Add(skala);
            }
            
            ComboBoxSkala_Lohnfortzahlung.SelectedIndex = 0;
        }

        private void ButtonCalculateKuendigungsfrist_Click(object sender, RoutedEventArgs e)
        {
            DateTime? eintrittsDatum = DatePickerEintrittsdatum_Kuendigungsfrist.SelectedDate ?? DateTime.MinValue;
            DateTime? kuendigungsDatum = DatePickerKuendigungsdatum_Kuendigungsfrist.SelectedDate ?? DateTime.MinValue;

            string kuendigungsfrist = KuendigungsfristBerrechnen(eintrittsDatum, kuendigungsDatum);
            OutputKuendigungsdatum.Content = $"Kündigungsfrist: {kuendigungsfrist}";
        }

        private void ButtonCalculateLohnfortzahlung_Click(object sender, RoutedEventArgs e)
        {
            DateTime? eintrittsDatum = DatePickerEintrittsdatum_Lohnfortzahlung.SelectedDate ?? DateTime.MinValue;
            DateTime? erkrankungsDatum = DatePickerKrankheitsdatum_Lohnfortzahlung.SelectedDate ?? DateTime.MinValue;
            string? skala = ComboBoxSkala_Lohnfortzahlung.SelectedItem as string;

            string lohnfortzahlung = LohnfortzahlungBerrechnen(eintrittsDatum, erkrankungsDatum, skala);

            OutputLohnfortzahlung.Content = $"Lohnfortzahlung: {lohnfortzahlung}";
        }

        private void ButtonGoBack_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.Content = null;
        }

        string[] kuendigungsfristen = { "1 Monat", "2 Monate", "3 Monate" };

        

        public string KuendigungsfristBerrechnen(DateTime? eintrittsdatum, DateTime? kuendigungsdatum)
        {
            TimeSpan? unterschied = kuendigungsdatum - eintrittsdatum;

            if(unterschied == null) return "Kuendigungsfrist nicht berechenbar";

            switch (unterschied?.Days)
            {
                case <= 365:

                    return kuendigungsfristen[0];

                case > 365 and < 3285:

                    return kuendigungsfristen[1];
                case > 3285:

                    return kuendigungsfristen[2];

                default:
                    return "Kuendigungsfrist nicht berechenbar";
            }
        }

        public string LohnfortzahlungBerrechnen(DateTime? eintrittsdatum, DateTime? kuendigungsdatum, string skala)
        {
            TimeSpan? unterschied = kuendigungsdatum - eintrittsdatum;

            if(unterschied == null) return "Lohnfortzahlung nicht berechenbar";

            if (skala == skalen[0])
            {
                switch (unterschied?.Days)
                {
                    case <= 365:

                        return "3 Wochen";

                    case > 365 and <= 730:

                        return "1 Monat";


                    case > 730 and <= 1460:

                        return "2 Monate ";

                    case > 1460 and <= 3285:

                        return "3 Monate";
                    case > 3285 and <= 5110:

                        return "4 Monate";
                    case > 5110 and <= 6935:

                        return "5 Monate";
                    case > 6935:
                        return "6 Monate";

                    default:
                        return "Lohnfortzahlung nicht berechenbar";
                }
            }
            else if (skala == skalen[1])
            {
                switch (unterschied?.Days)
                {
                    case <= 365:

                        return "3 Wochen";

                    case > 365 and <= 1095:

                        return "2 Monat";
                    case > 1095 and <= 3650:

                        return "3 Monate";
                    case > 3650:

                        return "4 Monate";
                }

            }
            else if (skala == skalen[2])
            {
                switch (unterschied?.Days)
                {
                    case <= 365:

                        return "3 Wochen";

                    case > 365 and <= 730:

                        return "8 Wochen";
                    case > 730 and <= 1095:

                        return "9 Wochen";
                    case > 1095 and <= 1460:

                        return "10 Wochen";
                    case > 1460 and <= 1825:

                        return "11 Wochen";
                    case > 1825 and <= 2190:

                        return "12 Wochen";
                    case > 2190 and <= 2555:

                        return "13 Wochen";
                    case > 2555 and <= 2920:

                        return "14 Wochen";

                    case > 2920 and <= 3285:

                        return "15 Wochen";
                    case > 3285 and <= 3650:

                        return "16 Wochen";
                    case > 3650 and <= 4015:

                        return "17 Wochen";
                    default:
                        return "Lohnfortzahlung nicht berechenbar";

                }
            }
            return "Lohnfortzahlung nicht berechenbar";


        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!(CheckBox1.IsChecked ?? false) && !(CheckBox2.IsChecked ?? false) && !(CheckBox3.IsChecked ?? false) && !(CheckBox4.IsChecked ?? false))
            {
                OutputNichtigeKuendigung.Content = string.Empty;
            }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            OutputNichtigeKuendigung.Content = "Die Kündigung ist nichtig";
        }

        
    }
}
