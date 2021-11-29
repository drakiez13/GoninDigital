using GoninDigital.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace GoninDigital.Converters
{
    internal class SumProductsPrice : IValueConverter
    {
        public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable<Cart> selectedCarts = values as IEnumerable<Cart>;
            long sum = 0;
            foreach (var selected in selectedCarts)
            {
                sum += selected.Quantity * selected.Product.Price;
            }
                
            return sum;
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
