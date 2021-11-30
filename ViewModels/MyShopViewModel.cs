using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GoninDigital.Models;
using GoninDigital.Properties;
using Microsoft.EntityFrameworkCore;

namespace GoninDigital.ViewModels
{
    class MyShopViewModel : BaseViewModel
    {
        private Vendor vendor=null;

        public Vendor Vendor
        {
            get { return vendor; }
            set { vendor = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Product> products=null;
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set { products = value; OnPropertyChanged(); }
        }

        private void InitVendor()
        {
            using (var db = new GoninDigitalDBContext())
            {
                try
                {
                    Vendor = db.Vendors.Include(o => o.Owner)
                        .Include(o=>o.Products)
                        .Single(o => o.Owner.UserName == Settings.Default.usrname);
                    /*Products = db.Products.Where(o => o.VendorId == Vendor.Id).ToList();*/
                    Products=new ObservableCollection<Product>( Vendor.Products.ToList());
                }
                catch
                {

                    //MessageBox.Show("Cannot find out any vendors ");
                }
            }
        }
        public void OnNavigatedTo()
        {
            
        }
        public void EditProduct()
        {

        }
        public void RemoveProduct(Product product)
        {
            using(var db= new GoninDigitalDBContext())
            {
                try
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
                catch
                {

                    //MessageBox.Show("Cannot find out any vendors ");
                }
            }
        }
        public MyShopViewModel()
        {
            Thread thread = new Thread(InitVendor);
            thread.Start();

        }

    }
}
