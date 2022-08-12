using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Section_16___RSS_Reader_App.ViewModel.Commands
{
    public class GetRssCommand : ICommand
    {
        public MainVM MainVM { get; set; }

        public event EventHandler? CanExecuteChanged;

        public GetRssCommand(MainVM mainVM)
        {
            MainVM = mainVM;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            await MainVM.ReadRSS();
        }
    }
}
