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
    internal class SearchItemTypeToVisibility : IValueConverter
    {
        static int VENDOR = 0;
        static int PRODUCT = 1;
        static int NOTFOUND = 2;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (int)value;
            
            if (type == NOTFOUND)
                return Visibility.Collapsed;

            var control = (string)parameter;
            if (control == "True")
            {
                if (type == PRODUCT)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            else
            {
                if (type == VENDOR)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
