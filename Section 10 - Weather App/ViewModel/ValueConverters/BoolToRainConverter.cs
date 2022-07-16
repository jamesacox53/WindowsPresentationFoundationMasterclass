using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Section_10___Weather_App.ViewModel.ValueConverters
{
    public class BoolToRainConverter : IValueConverter
    {
        private string currentlyRaining = "It is Raining";
        private string notCurrentlyRaining = "It is not Raining";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isRaining = (bool) value;

            if (isRaining)
            {
                return currentlyRaining;
            }
            else
            {
                return notCurrentlyRaining;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string isRaining = (string) value;

            if (isRaining == currentlyRaining)
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
