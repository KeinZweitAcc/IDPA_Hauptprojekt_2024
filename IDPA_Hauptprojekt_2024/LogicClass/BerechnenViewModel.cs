using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace IDPA_Hauptprojekt_2024.LogicClass
{
    public class BerechnenViewModel : INotifyPropertyChanged
    {
        private readonly Berechnen _berechnen;

        public BerechnenViewModel()
        {
            _berechnen = new Berechnen();
            CalculateKuendigungsfristCommand = new RelayCommand(_ => CalculateKuendigungsfrist());
            CalculateLohnfortzahlungCommand = new RelayCommand(_ => CalculateLohnfortzahlung());

          
            Skalen = new ObservableCollection<string> { "Bern", "Basel", "Zürich" };
            SelectedSkala = Skalen[0]; 
        }

        private DateTime? _eintrittsdatumKuendigungsfrist;
        public DateTime? EintrittsdatumKuendigungsfrist
        {
            get => _eintrittsdatumKuendigungsfrist;
            set { _eintrittsdatumKuendigungsfrist = value; OnPropertyChanged(); }
        }

        private DateTime? _kuendigungsdatumKuendigungsfrist;
        public DateTime? KuendigungsdatumKuendigungsfrist
        {
            get => _kuendigungsdatumKuendigungsfrist;
            set { _kuendigungsdatumKuendigungsfrist = value; OnPropertyChanged(); }
        }

        private string _kuendigungsfristResult;
        public string KuendigungsfristResult
        {
            get => _kuendigungsfristResult;
            set { _kuendigungsfristResult = value; OnPropertyChanged(); }
        }

        
        private DateTime? _eintrittsdatumLohnfortzahlung;
        public DateTime? EintrittsdatumLohnfortzahlung
        {
            get => _eintrittsdatumLohnfortzahlung;
            set { _eintrittsdatumLohnfortzahlung = value; OnPropertyChanged(); }
        }

        private DateTime? _krankheitsdatumLohnfortzahlung;
        public DateTime? KrankheitsdatumLohnfortzahlung
        {
            get => _krankheitsdatumLohnfortzahlung;
            set { _krankheitsdatumLohnfortzahlung = value; OnPropertyChanged(); }
        }

        private string _selectedSkala;
        public string SelectedSkala
        {
            get => _selectedSkala;
            set { _selectedSkala = value; OnPropertyChanged(); }
        }

        private string _lohnfortzahlungResult;
        public string LohnfortzahlungResult
        {
            get => _lohnfortzahlungResult;
            set { _lohnfortzahlungResult = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> Skalen { get; }

        private bool _militaerdienstChecked;
        public bool MilitaerdienstChecked
        {
            get => _militaerdienstChecked;
            set { _militaerdienstChecked = value; OnPropertyChanged(); CheckSpecialCases(); }
        }

        private bool _krankheitChecked;
        public bool KrankheitChecked
        {
            get => _krankheitChecked;
            set { _krankheitChecked = value; OnPropertyChanged(); CheckSpecialCases(); }
        }

        private bool _schwangerschaftChecked;
        public bool SchwangerschaftChecked
        {
            get => _schwangerschaftChecked;
            set { _schwangerschaftChecked = value; OnPropertyChanged(); CheckSpecialCases(); }
        }

        private bool _hilfsaktionChecked;
        public bool HilfsaktionChecked
        {
            get => _hilfsaktionChecked;
            set { _hilfsaktionChecked = value; OnPropertyChanged(); CheckSpecialCases(); }
        }

        private string _spezialfallResult;
        public string SpezialfallResult
        {
            get => _spezialfallResult;
            set { _spezialfallResult = value; OnPropertyChanged(); }
        }

    
        public ICommand CalculateKuendigungsfristCommand { get; }
        public ICommand CalculateLohnfortzahlungCommand { get; }

      
        private void CalculateKuendigungsfrist()
        {
            KuendigungsfristResult = _berechnen.KuendigungsfristBerrechnen(EintrittsdatumKuendigungsfrist, KuendigungsdatumKuendigungsfrist);
        }

        private void CalculateLohnfortzahlung()
        {
            LohnfortzahlungResult = _berechnen.LohnfortzahlungBerrechnen(EintrittsdatumLohnfortzahlung, KrankheitsdatumLohnfortzahlung, SelectedSkala);
        }

        
        private void CheckSpecialCases()
        {
            if (MilitaerdienstChecked || KrankheitChecked || SchwangerschaftChecked || HilfsaktionChecked)
            {
                SpezialfallResult = "Die Kündigung ist nichtig.";
            }
            else
            {
                SpezialfallResult = string.Empty;
            }
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}