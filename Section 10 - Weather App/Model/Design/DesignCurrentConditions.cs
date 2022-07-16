using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section_10___Weather_App.Model.Design
{
    public class DesignCurrentConditions : CurrentConditions
    {
        public DesignCurrentConditions()
        {
            WeatherText = "Partly Cloudy";
            HasPrecipitation = true;
            Temperature = new Temperature()
            {
                Metric = new Metric()
                {
                    Value = "25"
                }
            };
        }
    }
}
