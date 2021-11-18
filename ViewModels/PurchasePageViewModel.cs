using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GoninDigital.Models;

namespace GoninDigital.ViewModels
{
    class PurchasePageViewModel : BaseViewModel
    {
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
        /*private static List<Product> metaProducts = new List<Product> {
            new Product { Name="Product 1", Price=100000},
            new Product { Name="Product 2", Price=200000},
            new Product { Name="Product 3", Price=300000},
            new Product { Name="Product 4", Price=400000},
            new Product { Name="Product 5", Price=400000}
        };*/
        public PurchasePageViewModel()
        {
            GoninDigitalDBContext db = DataProvider.Instance.Db;
            recommnededByEditor = db.Products.ToList();
            /*recommnededByEditor = metaProducts;*/
        }
    }
}
