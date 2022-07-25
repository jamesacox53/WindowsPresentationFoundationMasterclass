using Section_11___Notes_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Section_11___Notes_App.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        public LoginVM LoginVM { get; set; }
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RegisterCommand(LoginVM loginVM)
        {
            LoginVM = loginVM;
        }

        public bool CanExecute(object? parameter)
        {
            User? user = (parameter as User);

            if (user == null) return false;

            if (string.IsNullOrWhiteSpace(user.Username)) return false;

            if (string.IsNullOrWhiteSpace(user.Password)) return false;

            if (string.IsNullOrWhiteSpace(user.ConfirmPassword)) return false;

            return (string.Equals(user.Password, user.ConfirmPassword));
        }

        public void Execute(object? parameter)
        {
            LoginVM.Register();
        }
    }
}
