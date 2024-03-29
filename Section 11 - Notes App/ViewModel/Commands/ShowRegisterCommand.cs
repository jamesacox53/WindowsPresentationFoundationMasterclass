﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Section_11___Notes_App.ViewModel.Commands
{
    public class ShowRegisterCommand : ICommand
    {
        public LoginVM LoginVM { get; set; }
        public event EventHandler? CanExecuteChanged;

        public ShowRegisterCommand(LoginVM loginVM)
        {
            LoginVM = loginVM;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            LoginVM.SwitchViews();
        }
    }
}
