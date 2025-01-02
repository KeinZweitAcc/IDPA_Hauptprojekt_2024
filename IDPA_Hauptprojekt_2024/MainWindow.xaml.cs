using IDPA_Hauptprojekt_2024.Database;
using IDPA_Hauptprojekt_2024_Migration.Database;
using System.Text;
using System.Windows;
using IDPA_Hauptprojekt_2024.LocigClass;

namespace IDPA_Hauptprojekt_2024
{
    public partial class MainWindow : Window
    {
        public DataContext _dataContext;
        private ViewModelClass _viewModel;

        private readonly FilterAlgorithm _filterAlgorithm;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModelClass();
            DataContext = _viewModel;

            ImportData();
            _filterAlgorithm = new FilterAlgorithm(_dataContext);
        }


        private void ImportData()
        {
            var factory = new DataContextFactory();
            _dataContext = factory.CreateDbContext(new string[0]);
            var dataImporter = new DataImporter(_dataContext);
            dataImporter.ImportData();
        }

        private void ButtonSubmitScenario_Click(object sender, RoutedEventArgs e)
        {
            var ListFiltredArticles = _filterAlgorithm.Filter(TextBoxScenario.Text);
            _viewModel.UpdateArticles(ListFiltredArticles);
        }
    }
}