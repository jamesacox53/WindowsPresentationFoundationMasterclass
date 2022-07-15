using Section_10___Weather_App.Model;
using Section_10___Weather_App.Model.Design;
using Section_10___Weather_App.ViewModel.Commands;
using Section_10___Weather_App.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section_10___Weather_App.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string query;

        public string Query
        {
            get { return query; }
            set 
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set
            {
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
            }
        }

        private CurrentConditions currentConditions;

        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set 
            { 
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }

        public SearchCommand SearchCommand { get; set; }

        public WeatherVM()
        {
            SearchCommand = new SearchCommand(this);
            
            if (!DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            SelectedCity = (new DesignCity());
            CurrentConditions = (new DesignCurrentConditions());
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public async void MakeQuery()
        {
            List<City> cities = await AccuweatherHelper.GetCitiesAsync(Query);
        }
    }
}
