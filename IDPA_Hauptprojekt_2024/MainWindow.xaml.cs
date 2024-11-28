using System.Windows;
using IDPA_Hauptprojekt_2024.LocigClass;

namespace IDPA_Hauptprojekt_2024
{
    public partial class MainWindow : Window
    {
        private FilterAlgorithm _filterAlgorithm;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void SetFilterAlgorithm(FilterAlgorithm filterAlgorithm)
        {
            _filterAlgorithm = filterAlgorithm;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (_filterAlgorithm != null)
            {
                var ListFiltredArticles =  _filterAlgorithm.Filter(TextBoxScenario.Text);
            }
            else
            {
                // Handle the case where _filterAlgorithm is not set
            }
        }
    }
}