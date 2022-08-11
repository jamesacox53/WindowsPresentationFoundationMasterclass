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
    public class RSSHelperVM
    {
        private const string finZenBlogURL = $"https://www.finzen.mx/blog-feed.xml";

        public async Task<List<Item>?> GetPosts()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(FinZenBlog));

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage result = await client.GetAsync(finZenBlogURL);
                string xmlResult = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlResult)))
                    {
                        FinZenBlog? finZenBlog = xmlSerializer.Deserialize(stream) as FinZenBlog;

                        if (finZenBlog == null) return null;

                        return GetItem(finZenBlog);
                    }
                }

                return null;
            }
        }

        private List<Item>? GetItem(FinZenBlog finZenBlog)
        {
            if (finZenBlog == null) return null;

            RSS rss = finZenBlog.RSS;

            if (rss == null) return null;

            Channel channel = rss.Channel;

            if (channel == null) return null;

            List<Item> item = channel.Item;

            return item;
        }
    }           
}
