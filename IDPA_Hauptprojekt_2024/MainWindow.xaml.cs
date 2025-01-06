using System.ComponentModel;
using IDPA_Hauptprojekt_2024.Database;
using IDPA_Hauptprojekt_2024_Migration.Database;
using System.Text;
using System.Windows;
using IDPA_Hauptprojekt_2024.LocigClass;

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
            MessageBox.Show("Jeder Artikel ist vom schweizer OR (20 Auflage 2024)");
        }
    }
}