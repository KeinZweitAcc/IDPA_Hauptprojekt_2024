using IDPA_Hauptprojekt_2024.Database;
using IDPA_Hauptprojekt_2024_Migration.Database;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IDPA_Hauptprojekt_2024
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataContext _dataContext;

        public MainWindow()
        {
            InitializeComponent();
            ImportData();
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

        }
    }
}