using Section_11___Notes_App.Model;
using Section_11___Notes_App.ViewModel.Commands;
using Section_11___Notes_App.ViewModel.Helpers.Database;
using Section_11___Notes_App.ViewModel.Helpers.Database.Firebase;
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
        private IUser user;
        public IUser User
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

        public event EventHandler Authenticated;

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
            User = Database.DatabaseHelper.CreateUser();

            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
            ShowRegisterCommand = new ShowRegisterCommand(this);

            SetInitialVisibilities();
        }

        private IUser CreateUserFromProperties()
        {
            IUser tempUser = Database.DatabaseHelper.CreateUser();
            tempUser.Username = this.Username;
            tempUser.Name = this.Name;
            tempUser.Lastname = this.Lastname;
            tempUser.Password = this.Password;
            tempUser.ConfirmPassword = this.ConfirmPassword;

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

        public async Task Login()
        {
            bool successfullyLogin = await Database.AuthHelper.Login(User);

            if (successfullyLogin)
            {
                Authenticated?.Invoke(this, new EventArgs());
            }
        }

        public async Task Register()
        {
            bool successfullyRegister = await Database.AuthHelper.Register(User);

            if (successfullyRegister)
            {
                Authenticated?.Invoke(this, new EventArgs());
            }
        }
    }
}
