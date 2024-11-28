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
using IDPA_Hauptprojekt_2024.Database.Logic;
using IDPA_Hauptprojekt_2024.Database.Model;
using IDPA_Hauptprojekt_2024.LocigClass;

namespace IDPA_Hauptprojekt_2024
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly FilterAlgorithm _filterAlgorithm;
        public MainWindow(FilterAlgorithm filterAlgorithm)
        {
            _filterAlgorithm = filterAlgorithm;
            InitializeComponent();

        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var ListFiltredArticles = _filterAlgorithm.Filter(InputTextBox.Text);

        }
    }
}