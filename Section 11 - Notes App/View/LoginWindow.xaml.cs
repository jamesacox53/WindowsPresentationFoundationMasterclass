using Section_11___Notes_App.ViewModel;
using Section_11___Notes_App.ViewModel.Helpers.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Section_11___Notes_App.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginVM loginVM;

        public LoginWindow()
        {
            InitializeComponent();

            loginVM = Resources["LoginVM"] as LoginVM;

            loginVM.Authenticated += LoginVM_Authenticated;
        }

        private void LoginVM_Authenticated(object? sender, EventArgs e)
        {
            NotesWindow notesWindow = new NotesWindow();

            notesWindow.Show();

            Close();
        }

        private void LoginComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = LoginComboBox.SelectedIndex;

            SetBothRegisterAndLoginComboBoxes(selectedIndex);
        }

        private void RegisterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = RegisterComboBox.SelectedIndex;

            SetBothRegisterAndLoginComboBoxes(selectedIndex);
        }

        private void SetBothRegisterAndLoginComboBoxes(int selectedIndex)
        {
            if ((RegisterComboBox != null) && (RegisterComboBox.SelectedIndex != selectedIndex))
            {
                RegisterComboBox.SelectedIndex = selectedIndex;
            }

            if ((LoginComboBox != null) && LoginComboBox.SelectedIndex != selectedIndex)
            {
                LoginComboBox.SelectedIndex = selectedIndex;
            }

            SetDatabaseHelper(selectedIndex);
        }

        private void SetDatabaseHelper(int selectedIndex)
        {
            if (selectedIndex == 0)
            {
                Database.UseLocal();
            }
            else
            {
                Database.UseFirebase();
            }
        }
    }
}
