using GoninDigital.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GoninDigital.Converters
{
    class BrandIdToName :IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var id = (int)value;
            string name;
            using (var db = new GoninDigitalDBContext())
            {
                name = db.Brands.Single(o => o.Id == id).Name;
            }
            return name;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var name = (string)value;
            int id;
            using (var db = new GoninDigitalDBContext())
            {
                id = db.Brands.Single(o => o.Name == name).Id;
            }
            return id;
        }
    }
}
