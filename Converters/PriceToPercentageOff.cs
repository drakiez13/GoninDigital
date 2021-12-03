using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GoninDigital.Converters
{
    class PriceToPercentageOff : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var price = (long)values[0];
                var originPrice = (long)values[1];
                if (price < originPrice)
                    return "-" + ((originPrice - price) * 100 / originPrice).ToString() + "%";
                return "";
            }
            catch
            {
                return "";
            }

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
