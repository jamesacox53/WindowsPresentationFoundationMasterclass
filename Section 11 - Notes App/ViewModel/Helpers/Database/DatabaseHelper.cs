using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section_11___Notes_App.ViewModel.Helpers.Database
{
    public sealed class DatabaseHelper
    {
        private static readonly IDatabaseHelper database = new FirebaseDatabaseHelper();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static DatabaseHelper()
        {
        }

        private DatabaseHelper()
        {
        }

        public static IDatabaseHelper Database
        {
            get
            {
                return database;
            }
        }
    }
}