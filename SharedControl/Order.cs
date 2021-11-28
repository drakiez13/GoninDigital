using GoninDigital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoninDigital.SharedControl
{
    class Order: BaseViewModel
    {
        private string image;
        public string Image
        {
            get { return image; }
            set { image = value; OnPropertyChanged();  }
        }
        private string vendorName;
        public string VendorName
        {
            get { return vendorName; }
            set { vendorName = value; OnPropertyChanged(); }
        }
        private string productName;
        public string ProductName
        {
            get { return productName; }
            set { productName = value; OnPropertyChanged(); }
        }
        private string brandName;
        public string BrandName
        {
            get { return brandName; }
            set { brandName = value; OnPropertyChanged(); }
        }
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; OnPropertyChanged(); }
        }
        private double priceDisc;
        public double PriceDisc
        {
            get { return priceDisc; }
            set { priceDisc = value; OnPropertyChanged(); }
        }
        private long priceOrg;
        public long PriceOrg
        {
            get { return priceOrg; }
            set { priceDisc = value; OnPropertyChanged(); }
        }
        private long totalPrice;
        public long TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; OnPropertyChanged(); }
        }
        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; OnPropertyChanged(); }
        }
        public Order(string VendorName, string ProductName, string BrandName, int Quantity, double PriceDisc, long PriceOrg, long TotalPrice, string Status)
        {
            this.VendorName = VendorName;
            this.ProductName = ProductName;
            this.BrandName = BrandName;
            this.Quantity = Quantity;
            this.PriceDisc = PriceDisc;
            this.PriceOrg = PriceOrg;
            this.TotalPrice = TotalPrice;
            this.Status = Status;
        }
    }
}
