using Section_11___Notes_App.Model;
using Section_11___Notes_App.ViewModel.Commands;
using Section_11___Notes_App.ViewModel.Helpers;
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
            set 
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set 
            {
                username = value;
                OnPropertyChanged("Username");

                User = CreateUserFromProperties();
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");

                User = CreateUserFromProperties();
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");

                User = CreateUserFromProperties();
            }
        }

        private string lastname;
        public string Lastname
        {
            get { return lastname; }
            set
            {
                lastname = value;
                OnPropertyChanged("Lastname");

                User = CreateUserFromProperties();
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");

                User = CreateUserFromProperties();
            }
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
            User = new User();

            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
            ShowRegisterCommand = new ShowRegisterCommand(this);

            SetInitialVisibilities();
        }

        private User CreateUserFromProperties()
        {
            User tempUser = new User()
            {
                Username = this.Username,
                Name = this.Name,
                Lastname = this.Lastname,
                Password = this.Password,
                ConfirmPassword = this.ConfirmPassword
            };

            return tempUser;
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

        public void Login()
        {

        }

        public async void Register()
        {
            FirebaseAuthHelper.Register(User);
        }
    }
}
