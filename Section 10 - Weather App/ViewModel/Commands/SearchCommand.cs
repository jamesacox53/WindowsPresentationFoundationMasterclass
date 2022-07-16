using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Section_10___Weather_App.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        public WeatherVM WeatherVM { get; set; }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SearchCommand(WeatherVM weatherVM)
        {
            this.WeatherVM = weatherVM;
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter == null) return false;
            string query = parameter.ToString();

            if (string.IsNullOrWhiteSpace(query)) return false;

            return true;
        }

        public void Execute(object? parameter)
        {
            WeatherVM.ClearCityWetherInfoFields();
            WeatherVM.MakeQuery();
        }
    }
}
