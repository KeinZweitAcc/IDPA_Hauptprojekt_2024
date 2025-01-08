// Updated Berechnungen.xaml.cs to handle Spezialfälle checkboxes using _checked functions
using System.Windows;
using System.Windows.Controls;
using IDPA_Hauptprojekt_2024.LogicClass;

namespace IDPA_Hauptprojekt_2024
{
    /// <summary>
    /// Interaction logic for Berechnungen.xaml
    /// </summary>
    public partial class Berechnungen : Page
    {
        private MainWindow _mainWindow;
        private readonly BerechnenViewModel _viewModel;

        public Berechnungen(MainWindow mainWindow)
        {
            InitializeComponent();
            _viewModel = new BerechnenViewModel();
            DataContext = _viewModel;
            _mainWindow = mainWindow;
        }

        private void ButtonGoBack_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.MainFrame.Navigate(null);
        }

        // Methods to handle checkbox _Checked events
        private void Militaerdienst_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel.MilitaerdienstChecked = ((CheckBox)sender).IsChecked == true;
        }

        private void Krankheit_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel.KrankheitChecked = ((CheckBox)sender).IsChecked == true;
        }

        private void Schwangerschaft_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel.SchwangerschaftChecked = ((CheckBox)sender).IsChecked == true;
        }

        private void Hilfsaktion_Checked(object sender, RoutedEventArgs e)
        {
            _viewModel.HilfsaktionChecked = ((CheckBox)sender).IsChecked == true;
        }

        // Methods to handle checkbox _Unchecked events
        private void Militaerdienst_Unchecked(object sender, RoutedEventArgs e)
        {
            _viewModel.MilitaerdienstChecked = ((CheckBox)sender).IsChecked == false;
        }

        private void Krankheit_Unchecked(object sender, RoutedEventArgs e)
        {
            _viewModel.KrankheitChecked = ((CheckBox)sender).IsChecked == false;
        }

        private void Schwangerschaft_Unchecked(object sender, RoutedEventArgs e)
        {
            _viewModel.SchwangerschaftChecked = ((CheckBox)sender).IsChecked == false;
        }

        private void Hilfsaktion_Unchecked(object sender, RoutedEventArgs e)
        {
            _viewModel.HilfsaktionChecked = ((CheckBox)sender).IsChecked == false;
        }
    }
}
