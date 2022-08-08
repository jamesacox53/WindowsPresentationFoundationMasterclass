using Section_11___Notes_App.ViewModel.Helpers.Database.Firebase;
using Section_11___Notes_App.ViewModel.Helpers.Database.SQLLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section_11___Notes_App.ViewModel.Helpers.Database
{
    public sealed class Database
    {
        private static readonly FirebaseDatabaseHelper firebaseDatabaseHelper = new FirebaseDatabaseHelper();
        private static readonly SQLLiteDatabaseHelper sqlLiteDatabaseHelper = new SQLLiteDatabaseHelper();

        private static IDatabaseHelper databaseHelper = firebaseDatabaseHelper;

        private static readonly FirebaseAuthHelper firebaseAuthHelper = new FirebaseAuthHelper();
        private static readonly SQLLiteAuthHelper sqlLiteAuthHelper = new SQLLiteAuthHelper();

        private static IAuthHelper authHelper = firebaseAuthHelper;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        public static void UseFirebase()
        {
            databaseHelper = firebaseDatabaseHelper;
            authHelper = firebaseAuthHelper;
        }

        public static void UseLocal()
        {
            databaseHelper = sqlLiteDatabaseHelper;
            authHelper = sqlLiteAuthHelper;
        }

        static Database()
        {
        }

        private Database()
        {
        }

        public static IDatabaseHelper DatabaseHelper
        {
            get
            {
                return databaseHelper;
            }
        }

        public static IAuthHelper AuthHelper
        {
            get
            {
                return authHelper;
            }
        }
    }
}