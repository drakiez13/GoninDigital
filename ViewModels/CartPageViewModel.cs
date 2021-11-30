using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GoninDigital.Models;
using GoninDigital.Properties;
using GoninDigital.SharedControl;
using GoninDigital.Views;
using GoninDigital.Views.SharedPages;
using Microsoft.EntityFrameworkCore;
using ModernWpf.Controls;

namespace GoninDigital.ViewModels
{
    class CartPageViewModel : BaseViewModel
    {
        private ObservableCollection<Cart> products;
        public ObservableCollection<Cart> Products
        {
            get { return products; }
            set { products = value; OnPropertyChanged(); }
        }
        private IEnumerable<Cart> selectedProducts;
        public IEnumerable<Cart> SelectedProducts
        {
            get { return selectedProducts; }
            set { selectedProducts = value; OnPropertyChanged(); }
        }

        public ICommand RemoveProduct { get; set; }
        public ICommand ShowProduct { get; set; }
        public ICommand BuyProduct { get; set; }

        private void Init()
        {
            using (var db = new GoninDigitalDBContext())
            {
                Products = new ObservableCollection<Cart>(db.Carts.Include(x => x.User)
                                .Include(x => x.Product)
                                .Include(x => x.Product.Vendor)
                                .Where(o => o.User.UserName == Settings.Default.usrname)
                                .ToList());
            }
        }

        private async void RemoveCartDb(Cart cart)
        {
            using (var db = new GoninDigitalDBContext())
            {
                db.Carts.Remove(cart);
                await db.SaveChangesAsync();
            }
        }


        public void OnNavigatedTo()
        {
            Thread thread = new Thread(Init);
            thread.Start();
        }

        public CartPageViewModel()
        {
            selectedProducts = new ObservableCollection<Cart>();
            RemoveProduct = new RelayCommand<Cart>(o => true, 
                cart => { Products.Remove(cart); RemoveCartDb(cart); });
            ShowProduct = new RelayCommand<Cart>(o => true, 
                cart => DashBoard.RootFrame.Navigate(new ProductPage(cart.ProductId)));
        }
    }

}