using Newtonsoft.Json;
using Section_11___Notes_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Section_11___Notes_App.ViewModel.Helpers
{
    public class FirebaseAuthHelper
    {
        private static string api_key = "APIKey";

        public static async Task<bool> Register(User user)
        {
            using(HttpClient client = new HttpClient())
            {
                var body = new
                {
                    email = user.Username,
                    password = user.Password,
                    returnSecureToken = true
                };

                string bodyJson = JsonConvert.SerializeObject(body);
                StringContent data = new StringContent(bodyJson, Encoding.UTF8, "application/json");
                string firebaseUrl = $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={api_key}";
                HttpResponseMessage response = await client.PostAsync(firebaseUrl, data);

                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    FirebaseResult? result = JsonConvert.DeserializeObject<FirebaseResult>(resultJson);

                    if (result == null) return false;

                    App.UserId = result.localId;

                    return true;
                }
                else
                {
                    string errorJson = await response.Content.ReadAsStringAsync();
                    FirebaseError? error = JsonConvert.DeserializeObject<FirebaseError>(errorJson);
                    if (error == null) return false;
                    
                    MessageBox.Show(error.error.message);

                    return false;
                }
            }
        }
    }

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
}
