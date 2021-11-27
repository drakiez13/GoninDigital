using System;
using System.Collections.Generic;
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
using Microsoft.EntityFrameworkCore;
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

        private void Init()
        {
            using (var db = new GoninDigitalDBContext())
            {
                Products = db.Carts.Include(x => x.User)
                                .Include(x => x.Product)
                                .Where(o => o.User.UserName == Settings.Default.usrname)
                                .Select(o => o.Product)
                                .ToList();
            }
        }

        public CartPageViewModel()
        {
            Thread thread = new Thread(Init);
            thread.Start();
        }
    }
}
