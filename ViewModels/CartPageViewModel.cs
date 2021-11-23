using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GoninDigital.Models;
using GoninDigital.Views;
using ModernWpf.Controls;

namespace GoninDigital.ViewModels
{
    class CartPageViewModel :BaseViewModel
    {
        
       

        private List<Product> products;
        public List<Product> Products
        {
            get { return products; }
            set { products = value; OnPropertyChanged(); }
        }
        
        public ICommand PurchaseCommand { get; set; }
        public ICommand RemoveCartItem { get; set; }
        public CartPageViewModel()
        {
  
            GoninDigitalDBContext db = DataProvider.Instance.Db;
            products = db.Products.ToList();

            PurchaseCommand = new RelayCommand<object>((p) => { return true; }, (p) => { DashBoard.RootFrame.Navigate(new CartPage_Purchase()); });
            RemoveCartItem = new RelayCommand<object>((p) => { return true; }, RemoveCartItemExe);
        }
        public void RemoveCartItemExe(object o)
        {
            
        }
    }
}
