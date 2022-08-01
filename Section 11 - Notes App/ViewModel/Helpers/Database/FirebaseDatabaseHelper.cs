using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Section_11___Notes_App.ViewModel.Helpers.Database
{
    public class FirebaseDatabaseHelper : IDatabaseHelper
    {
        private string dbPath = $"https://notes-app-wpf-c4211-default-rtdb.firebaseio.com/";

        public string DBPath 
        { 
            get 
            {
                return dbPath;
            } 
        }

        public Task<bool> Delete<T>(T item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Insert<T>(T item)
        {
            string jsonBody = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                string url = $"{DBPath}{item.GetType().Name.ToLower()}.json";
                var result = await client.PostAsync(url, content);

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

 
        }

        public Task<List<T>> Read<T>() where T : new()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update<T>(T item)
        {
            throw new NotImplementedException();
        }
    }
}
