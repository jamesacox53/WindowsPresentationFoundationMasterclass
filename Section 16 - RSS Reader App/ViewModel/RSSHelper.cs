using Section_16___RSS_Reader_App.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Section_16___RSS_Reader_App.ViewModel
{
    public class RSSHelper : IRSSHelper
    {
        private const string yahooNewsURL = $"https://www.yahoo.com/news/rss";

        public async Task<List<Item>?> GetPosts()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(YahooNews));

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage result = await client.GetAsync(yahooNewsURL);
                string xmlResult = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlResult)))
                    {
                        YahooNews? yahooNews = xmlSerializer.Deserialize(stream) as YahooNews;

                        if (yahooNews == null) return null;

                        return GetItem(yahooNews);
                    }
                }

                return null;
            }
        }

        private List<Item>? GetItem(YahooNews yahooNews)
        {
            if (yahooNews == null) return null;

            Channel channel = yahooNews.Channel;

            if (channel == null) return null;

            List<Item> item = channel.Item;

            return item;
        }
    }           
}
