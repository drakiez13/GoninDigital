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
        public string k;
        private Product _a;
        public Product a
        {
            get { return _a; }
            set { _a = value; OnPropertyChanged(); }
        }
        public HomePageViewModel()
        {
            var db = DataProvider.Instance.Db;
            _a = db.Products.First();
            db.Brands.ToList();
        }

    }
}
