using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Section_16___RSS_Reader_App.Model
{
    [XmlRoot(ElementName = "rss")]
    public class YahooNews
    {
        [XmlElement(ElementName = "channel")]
        public Channel Channel { get; set; }
    }

    [XmlRoot(ElementName = "channel")]
    public class Channel
    {
        [XmlElement(ElementName = "item")]
        public List<Item> Item { get; set; }
    }

    [XmlRoot(ElementName = "item")]
    public class Item
    {
        [XmlElement(ElementName = "title")]
        public String Title { get; set; }
        [XmlElement(ElementName = "link")]
        public string Link { get; set; }
       
        private string pubDate;
        [XmlElement(ElementName = "pubDate")]
        public string PubDate
        {
            get { return pubDate; }
            set
            {
                pubDate = value;
                PublishedDate = DateTime.Parse(value);
            }
        }

        public DateTime PublishedDate { get; set; }        
    }
}
