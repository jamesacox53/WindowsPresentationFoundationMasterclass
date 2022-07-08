using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Path = System.IO.Path;

namespace Section_06___Contacts_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string databaseName = "Contacts.db";
        public static string databasePath = Path.Combine(Environment.CurrentDirectory, databaseName);

    }
}
