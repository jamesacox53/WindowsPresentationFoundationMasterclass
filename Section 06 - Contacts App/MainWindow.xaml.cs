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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Section_06___Contacts_App.Classes;
using SQLite;

namespace Section_06___Contacts_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Contact> contacts = new List<Contact>();
        
        public MainWindow()
        {
            InitializeComponent();

            ReadDatabase();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();

            newContactWindow.ShowDialog();

            ReadDatabase();
        }

        private void ReadDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();
                contacts = connection.Table<Contact>().OrderBy(c => c.Name).ToList();
            }

            if (contacts == null) return;

            contactsListView.ItemsSource = contacts;
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Contact> filteredList;

            string text = searchTextBox.Text;

            if (text == null || text == "")
            {
                filteredList = contacts;
            }
            else
            {
                filteredList = contacts.Where<Contact>(c => DoesContactContainText(c, text)).ToList();
            }

            contactsListView.ItemsSource = filteredList;
        }

        private bool DoesContactContainText(Contact contact, string text)
        {
            if (contact == null || text == null) return false;

            string lowerCaseText = text.ToLower();

            if (contact.Name != null)
            {
                string lowerCaseName = contact.Name.ToLower();
                if (lowerCaseName.Contains(lowerCaseText)) return true;
            }

            if (contact.Email != null)
            {
                string lowerCaseEmail = contact.Email.ToLower();
                if (lowerCaseEmail.Contains(lowerCaseText)) return true;
            }

            if (contact.Phone != null)
            {
                string lowerCasePhone = contact.Phone.ToLower();
                if (lowerCasePhone.Contains(lowerCaseText)) return true;
            }

            return false;
        }

        private void contactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object selectedItem = contactsListView.SelectedItem;

            if (selectedItem == null) return;

            Contact selectedContact = (Contact) selectedItem;

            if (selectedContact == null) return;

            ContactDetailsWindow contactDetailsWindow = new ContactDetailsWindow(selectedContact);

            contactDetailsWindow.ShowDialog();

            ReadDatabase();
        }
    }
}
