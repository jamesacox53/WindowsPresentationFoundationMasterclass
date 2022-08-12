using Section_16___RSS_Reader_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section_16___RSS_Reader_App.ViewModel
{
    public interface IRSSHelper
    {
        public Task<List<Item>?> GetPosts();

    }
}
