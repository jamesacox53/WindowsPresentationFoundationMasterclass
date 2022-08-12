using Section_16___RSS_Reader_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section_16___RSS_Reader_App.ViewModel
{
    public class FakeRSSHelper : IRSSHelper
    {
        public async Task<List<Item>?> GetPosts()
        {
            Item exampleItem1 = new Item()
            {
                Title = "Example1",
                Link = "https://www.fakelink1.com",
                PublishedDate = new DateTime(2021, 10, 22)
            };

            Item exampleItem2 = new Item()
            {
                Title = "Example2",
                Link = "https://www.fakelink2.com",
                PublishedDate = new DateTime(2021, 10, 23)
            };

            List<Item> items = new List<Item>()
            {
                exampleItem1,
                exampleItem2
            };

            return items;
        }

    }
}
