using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GoninDigital.Models;
using GoninDigital.Properties;

namespace GoninDigital.ViewModels
{
    class MyShopViewModel:BaseViewModel
    {
        private string avatar;
        public string Avatar
        {
            get { return avatar; }
            set { avatar = value; OnPropertyChanged(); }
        }
        private string cover;
        public string Cover
        {
            get { return cover; }
            set { cover = value; OnPropertyChanged(); }
        }
        private string mail;
        public string Mail
        {
            get { return mail; }
            set { mail = value; OnPropertyChanged(); }
        }
        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged(); }
        }
        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(); }
        }
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }
        private List<Product> productList;
        public List<Product> ProductList
        {
            get { return productList; }
            set { productList = value; OnPropertyChanged(); }
        }


        

        public MyShopViewModel()
        {
            
            GoninDigitalDBContext db = DataProvider.Instance.Db;
            var usrname = Settings.Default.usrname;
            var users = db.Users;
            var vendors = db.Vendors;
            var products = db.Products;

            var usrId=from usr in users
                      where usr.UserName == usrname
                      select usr.Id;
            var vendorId = from usr in users
                           join vendor in vendors on usr.Id equals vendor.OwnerId
                           select vendor.Id;
            /*var vendorId = from vendor in vendors
                        where vendor.OwnerId == usrId
                        select vendor.Id;*/


            var ketqua = from product in products
                         join vendor in vendors on product.Id equals vendor.Id
                         where vendor.Id == vendor
                         select product;
            productList = ketqua.ToList();
            GoninDigitalDBContext db = DataProvider.Instance.Db;
            var usrname = Settings.Default.usrname;
            var users = db.Users;
            var vendors = db.Vendors;
            var products = db.Products;

            var usrId = from usr in users
                        where usr.UserName == usrname
                        select usr.Id;
            var vendorId = from usr in users
                           join vendor in vendors on usr.Id equals vendor.OwnerId
                           select vendor.Id;
            /*var vendorId = from vendor in vendors
                        where vendor.OwnerId == usrId
                        select vendor.Id;*/


            var ketqua = from product in products
                         join vendor in vendors on product.Id equals vendor.Id
                         where vendor.Id == vendor
                         select product;
            productList = ketqua.ToList();
            GoninDigitalDBContext db = DataProvider.Instance.Db;
            var usrname = Settings.Default.usrname;
            var users = db.Users;
            var vendors = db.Vendors;
            var products = db.Products;

            var usrId = from usr in users
                        where usr.UserName == usrname
                        select usr.Id;
            var vendorId = from usr in users
                           join vendor in vendors on usr.Id equals vendor.OwnerId
                           select vendor.Id;
            /*var vendorId = from vendor in vendors
                        where vendor.OwnerId == usrId
                        select vendor.Id;*/


            var ketqua = from product in products
                         join vendor in vendors on product.Id equals vendor.Id
                         where vendor.Id == vendor
                         select product;
            productList = ketqua.ToList();
        }

    }
}
