using IDPA_Hauptprojekt_2024.Database.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDPA_Hauptprojekt_2024.LocigClass
{
    public class ViewModelClass : INotifyPropertyChanged
    {
        public ObservableCollection<Articles> Articles { get; set; }

        public ViewModelClass()
        {
            Articles = new ObservableCollection<Articles>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateArticles(List<Articles> articles)
        {
            Articles.Clear();

            foreach (var article in articles)
            {
                Articles.Add(article);
            }
            OnPropertyChanged(nameof(Articles));
        }
    }
}