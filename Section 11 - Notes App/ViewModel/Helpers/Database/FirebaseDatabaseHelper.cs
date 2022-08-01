using Newtonsoft.Json;
using Section_11___Notes_App.Model;
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

        public async Task<List<T>> Read<T>() where T : new()
        {
            using (var client = new HttpClient())
            {
                string url = $"{DBPath}{typeof(T).Name.ToLower()}.json";
                var result = await client.GetAsync(url);
                string jsonResult = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    Dictionary<string, T>? objects = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonResult);
                    if (objects == null) return null;

                    List<T> values = new List<T>();

                    foreach(KeyValuePair<string, T> dictionaryEntry in objects)
                    {
                        values.Add(dictionaryEntry.Value);
                    }

                    return values;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<bool> Update<T>(T item) where T : IHasId
        {
            string jsonBody = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                string url = $"{DBPath}{item.GetType().Name.ToLower()}/{item.Id}.json";
                var result = await client.PatchAsync(url, content);

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

        public async Task<bool> Delete<T>(T item) where T : IHasId
        {
            using (var client = new HttpClient())
            {
                string url = $"{DBPath}{item.GetType().Name.ToLower()}/{item.Id}.json";
                var result = await client.DeleteAsync(url);

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
    }
}
