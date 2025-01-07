using System.Windows;
using System.Windows.Controls;

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

        private void InitializeSkalen()
        {
            ComboBoxSkala_Lohnfortzahlung.Items.Clear();
            ComboBoxSkala_Lohnfortzahlung.Items.Add("Basel");
            ComboBoxSkala_Lohnfortzahlung.Items.Add("Bern");
            ComboBoxSkala_Lohnfortzahlung.Items.Add("Zürich");
            ComboBoxSkala_Lohnfortzahlung.SelectedIndex = 0;
        }

        private void ButtonCalculateKuendigungsfrist_Click(object sender, RoutedEventArgs e)
        {
            DateTime? eintrittsDatum = DatePickerEintrittsdatum_Kuendigungsfrist.SelectedDate ?? DateTime.MinValue;
            DateTime? kuendigungsDatum = DatePickerKuendigungsdatum_Kuendigungsfrist.SelectedDate ?? DateTime.MinValue;

            OutputKuendigungsdatum.Content = $"OUTPUT {eintrittsDatum} - {kuendigungsDatum}";
        }

        private void ButtonCalculateLohnfortzahlung_Click(object sender, RoutedEventArgs e)
        {
            DateTime? eintrittsDatum = DatePickerEintrittsdatum_Lohnfortzahlung.SelectedDate ?? DateTime.MinValue;
            DateTime? erkrankungsDatum = DatePickerKrankheitsdatum_Lohnfortzahlung.SelectedDate ?? DateTime.MinValue;
            string? skala = ComboBoxSkala_Lohnfortzahlung.SelectedItem as string;

            OutputLohnfortzahlung.Content = $"OUTPUT {eintrittsDatum} - {erkrankungsDatum} - {skala}";
        }

        private void ButtonGoBack_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.Content = null;
        }
    }
}
