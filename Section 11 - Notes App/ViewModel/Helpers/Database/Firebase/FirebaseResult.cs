using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section_11___Notes_App.ViewModel.Helpers.Database.Firebase
{
    public class FirebaseResult
    {
        public string kind { get; set; }
        public string idToken { get; set; }
        public string email { get; set; }
        public string refreshToken { get; set; }
        public string expiresIn { get; set; }
        public string localId { get; set; }
    }

    public class FirebaseErrorDetails
    {
        public int code { get; set; }
        public string message { get; set; }
    }

    public class FirebaseError
    {
        public FirebaseErrorDetails error { get; set; }
    }

    public class FirebaseInsert
    {
        public string name { get; set; }
    }
}
