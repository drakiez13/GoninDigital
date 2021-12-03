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
        private string priceDisc;
        public string PriceDisc
        {
            get { return priceDisc; }
            set { priceDisc = value; OnPropertyChanged(); }
        }
        private string priceOrg;
        public string PriceOrg
        {
            get { return priceOrg; }
            set { priceOrg = value; OnPropertyChanged(); }
        }
        private string totalPrice;
        public string TotalPrice
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
        private string date;
        public string Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(); }
        }
        private int invoiceId;
        public int InvoiceId
        {
            get { return invoiceId; }
            set { invoiceId = value; OnPropertyChanged(); }
        }
    }
}
