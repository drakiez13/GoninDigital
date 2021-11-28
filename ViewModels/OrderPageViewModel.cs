using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoninDigital.Models;
using GoninDigital.SharedControl;
using GoninDigital.Properties;
using GoninDigital.Views;
using ModernWpf.Controls;

namespace GoninDigital.ViewModels
{
    class OrderPageViewModel: BaseViewModel
    {
        private List<Order> l_Order;
        public List<Order> L_Order
        {
            get { return l_Order; }
            set { l_Order = value; OnPropertyChanged(); }
        }
        private List<Invoice> l_Invoice;
        public List<Invoice> L_Invoice
        {
            get { return l_Invoice; }
            set { l_Invoice = value; OnPropertyChanged(); }
        }
        private List<InvoiceDetail> l_Invoice_Detail;
        public List<InvoiceDetail> L_Invoice_Detail
        {
            get { return l_Invoice_Detail; }
            set { l_Invoice_Detail = value; OnPropertyChanged(); }
        }
        private OrderCard selectedOrder;
        public OrderCard SelectedOrder
        {
            get { return selectedOrder; }
            set { selectedOrder = value; OnPropertyChanged(); }
        }
        public OrderPageViewModel()
        {
            GoninDigitalDBContext db = DataProvider.Instance.Db;
            //    int userID = db.Users.Where(x => x.UserName == Settings.Default.usrname).First().Id;
            L_Order = new List<Order>();
            int userID = 4;
            L_Invoice = db.Invoices.Where(x => x.CustomerId == userID).ToList();
           foreach(Invoice invoice in L_Invoice)
            {
                L_Invoice_Detail = db.InvoiceDetails.Where(x => x.InvoiceId == invoice.Id).ToList();
                foreach(InvoiceDetail invoicedt in L_Invoice_Detail)
                {
                    Product product = db.Products.Where(x => x.Id == invoicedt.ProductId).First();
                    string Image = product.Image;
                    string VendorName = db.Vendors.Where(x => x.Id == product.VendorId).First().Name;
                    string ProductName = product.Name;
                    string BrandName = db.Brands.Where(x => x.Id == product.BrandId).First().Name;
                    int Quantity = invoicedt.Quantity;
                    long PriceOrg = product.Price;
                    double PriceDisc = Convert.ToDouble(PriceOrg) * (1-Convert.ToDouble(product.DiscountRate) / 100);
                    long TotalPrice = (long)invoicedt.Cost;
                    string Status = db.InvoiceStatuses.Where(x => x.Id == invoice.StatusId).First().Name;
                    L_Order.Add(new Order(VendorName, ProductName, BrandName, Quantity, PriceDisc, PriceOrg, TotalPrice, Status));
                }
            }

        }
    }
}
