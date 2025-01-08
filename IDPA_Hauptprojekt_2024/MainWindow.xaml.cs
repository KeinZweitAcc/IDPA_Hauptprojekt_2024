using System.ComponentModel;
using System.Windows;
using IDPA_Hauptprojekt_2024.LogicClass;

namespace IDPA_Hauptprojekt_2024
{
    public partial class MainWindow : Window
    {
        private ViewModelClass _viewModel;

        private readonly FilterAlgorithm _filterAlgorithm;

        public MainWindow(FilterAlgorithm filterAlgorithm)
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            _viewModel = new ViewModelClass();
            DataContext = _viewModel;

            _filterAlgorithm = filterAlgorithm;
        }

        private void ButtonSubmitScenario_Click(object sender, RoutedEventArgs e)
        {
            NoArticleMessage.Visibility = Visibility.Hidden;

            var ListFiltredArticles = _filterAlgorithm.Filter(TextBoxScenario.Text);

            if (ListFiltredArticles.Count <= 0)
            {
                NoArticleMessage.Visibility = Visibility.Visible;
            }

            _viewModel.UpdateArticles(ListFiltredArticles);
        }

        private void MainWindow_OnClosing(object? sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SourceButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Gesetzesartikel:\n\rErnst J. Schneiter\n\rZGB/OR Kaufmännische Ausgabe: Zivilgesetzbuch, Obligationenrecht, SchKG, BV und weitere Erlasse\n\r20. Aufl. (Zürich: Orell Füssli Juristische Medien, 2024)");
        }

        private void SourceButtonOpenCalculator_Click(object sender, RoutedEventArgs e)
        {
            Berechnungen berechnungen = new Berechnungen(this);
            MainFrame.Navigate(berechnungen);
        }
    }
}