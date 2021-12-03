using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace GoninDigital.Converters
{
    internal class ListCountToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var count = (int)value;
            var param = (string)parameter;
            if (param == "True")
            {
                if (count == 0)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
            else
            {
                if (count == 0)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
