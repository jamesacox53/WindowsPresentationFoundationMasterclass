using Section_16___RSS_Reader_App.Model;
using Section_16___RSS_Reader_App.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section_16___RSS_Reader_App.ViewModel
{
    public class MainVM
    {
        public ObservableCollection<Item> Items { get; set; }

        public GetRssCommand GetRssCommand { get; set; }

        private IRSSHelper rssHelper = new FakeRSSHelper();

        public MainVM()
        {
            Items = new ObservableCollection<Item>();

            GetRssCommand = new GetRssCommand(this);
        }

        public async Task ReadRSS() 
        {
            Items.Clear();

            List<Item>? items = await rssHelper.GetPosts();

            if (items == null) return;

            foreach(Item item in items)
            {
                Items.Add(item);
            }
        }
    }
}
