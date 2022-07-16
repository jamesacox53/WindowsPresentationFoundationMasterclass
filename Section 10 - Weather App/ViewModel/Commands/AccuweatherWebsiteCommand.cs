using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Section_10___Weather_App.ViewModel.Commands
{
    public class AccuweatherWebsiteCommand : ICommand
    {
        public WeatherVM WeatherVM { get; set; }

        public event EventHandler? CanExecuteChanged;

        public AccuweatherWebsiteCommand(WeatherVM weatherVM)
        {
            this.WeatherVM = weatherVM;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var uri = "http://www.accuweather.com";
            var psi = new System.Diagnostics.ProcessStartInfo();
            psi.UseShellExecute = true;
            psi.FileName = uri;
            System.Diagnostics.Process.Start(psi);
        }
    }
}
