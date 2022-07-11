using Newtonsoft.Json;
using Section_10___Weather_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Section_10___Weather_App.ViewModel.Helpers
{
    public class AccuweatherHelper
    {
        private const string ACCUWEATHER_API_KEY = "5kdK73LpAUYiTCerTrQproRi4R5NFGbD";
        private const string AUTO_COMPLETE_BASE_API_URL = $"http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey={ACCUWEATHER_API_KEY}&q={{0}}";
        private const string CURRENT_CONDITIONS_BASE_API_URL = $"http://dataservice.accuweather.com/currentconditions/v1/{{0}}?apikey={ACCUWEATHER_API_KEY}";

        public static async Task<List<City>> GetCitiesAsync(string query) 
        {
            string autoCompleteURL = string.Format(AUTO_COMPLETE_BASE_API_URL, query);

            using (HttpClient client = new HttpClient()) 
            {
                HttpResponseMessage response = await client.GetAsync(autoCompleteURL);

                string json = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<City>>(json);
            }
        }

        public static async Task<CurrentConditions> GetCurrentConditionsAsync(string cityKey) 
        {
            string currentConditionsURL = string.Format(CURRENT_CONDITIONS_BASE_API_URL, cityKey);

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(currentConditionsURL);

                string json = await response.Content.ReadAsStringAsync();

                List<CurrentConditions> listOfCurrentConditions = JsonConvert.DeserializeObject<List<CurrentConditions>>(json);

                return listOfCurrentConditions[0];
            }
        }
    }
}
