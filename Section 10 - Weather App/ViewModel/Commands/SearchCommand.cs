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

        public event EventHandler? CanExecuteChanged;

        public SearchCommand(WeatherVM weatherVM)
        {
            this.WeatherVM = weatherVM;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            WeatherVM.MakeQuery();
        }
    }
}
