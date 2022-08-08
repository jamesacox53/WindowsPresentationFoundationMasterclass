using Newtonsoft.Json;
using Section_11___Notes_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Section_11___Notes_App.ViewModel.Helpers.Database.Firebase
{
    public class FirebaseAuthHelper : IAuthHelper
    {
        private string api_key = "ApiKey";

        public async Task<bool> Register(IUser user)
        {
            FirebaseUser? firebaseUser = user as FirebaseUser;

            if (firebaseUser == null) return false;

            using (HttpClient client = new HttpClient())
            {
                var body = new
                {
                    email = firebaseUser.Username,
                    password = firebaseUser.Password,
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

                    firebaseUser.LocalId = result.localId;

                    App.User = firebaseUser;

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

        public async Task<bool> Login(IUser user)
        {
            FirebaseUser? firebaseUser = user as FirebaseUser;

            if (firebaseUser == null) return false;

            using (HttpClient client = new HttpClient())
            {
                var body = new
                {
                    email = firebaseUser.Username,
                    password = firebaseUser.Password,
                    returnSecureToken = true
                };

                string bodyJson = JsonConvert.SerializeObject(body);
                StringContent data = new StringContent(bodyJson, Encoding.UTF8, "application/json");
                string firebaseUrl = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={api_key}";
                HttpResponseMessage response = await client.PostAsync(firebaseUrl, data);

                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    FirebaseResult? result = JsonConvert.DeserializeObject<FirebaseResult>(resultJson);

                    if (result == null) return false;

                    firebaseUser.LocalId = result.localId;

                    App.User = firebaseUser;

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
}
