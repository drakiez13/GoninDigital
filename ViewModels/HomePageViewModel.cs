using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GoninDigital.Models;

namespace GoninDigital.ViewModels
{
    class HomePageViewModel : BaseViewModel
    {
        private string art;
        public string Art
        {
            get { return art; }
            set { art = value; OnPropertyChanged(); }
        }

        private List<Product> recommnededByEditor;
        public List<Product> RecommendedByEditor
        {
            get { return recommnededByEditor; }
            set { recommnededByEditor = value; OnPropertyChanged(); }
        }
        public List<Product> RecommendedByEditor3
        {
            get { return recommnededByEditor.GetRange(0, 3); }
        }

        public HomePageViewModel()
        {
            art = "/GoninDigital;component/Resources/Images/HomeBanner.jpg";
            GoninDigitalDBContext db = DataProvider.Instance.Db;
            recommnededByEditor = db.Products.ToList();
        }

    }
}
