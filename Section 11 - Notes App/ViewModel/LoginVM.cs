using Section_11___Notes_App.Model;
using Section_11___Notes_App.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Section_11___Notes_App.ViewModel
{
    public class LoginVM : INotifyPropertyChanged
    {
        private User user;

        public User User
        {
            get { return user; }
            set { user = value; }
        }

        public RegisterCommand RegisterCommand { get; set; }
        public LoginCommand LoginCommand { get; set; }
        public ShowRegisterCommand ShowRegisterCommand { get; set; }

        private bool isShowingRegisterView;

        public event PropertyChangedEventHandler? PropertyChanged;

        private Visibility loginVisibility;

        public Visibility LoginVisibility
        {
            get { return loginVisibility; }
            set 
            {
                loginVisibility = value;
                OnPropertyChanged("LoginVisibility");
            }
        }

        private Visibility registerVisibility;

        public Visibility RegisterVisibility
        {
            get { return registerVisibility; }
            set
            {
                registerVisibility = value;
                OnPropertyChanged("RegisterVisibility");
            }
        }

        public LoginVM()
        {
            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
            ShowRegisterCommand = new ShowRegisterCommand(this);

            SetInitialVisibilities();
        }

        public void SwitchViews()
        {
            isShowingRegisterView = !isShowingRegisterView;

            if(isShowingRegisterView)
            {
                LoginVisibility = Visibility.Collapsed;
                RegisterVisibility = Visibility.Visible;
            }
            else
            {
                LoginVisibility = Visibility.Visible;
                RegisterVisibility = Visibility.Collapsed;
            }
        }

        private void SetInitialVisibilities()
        {
            LoginVisibility = Visibility.Visible;
            RegisterVisibility = Visibility.Collapsed;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
