using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoninDigital.Models;
using GoninDigital.SharedControl;
using System.Windows.Input;
using GoninDigital.Properties;
using GoninDigital.Views;
using ModernWpf.Controls;
using System.Windows;

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
        public ICommand AddtoCart { get; set; }
        public OrderPageViewModel()
        {
            GoninDigitalDBContext db = DataProvider.Instance.Db;
            L_Order = new List<Order>();
            //int userID = db.Users.Where(x => x.UserName == Settings.Default.usrname).First().Id;
            int userID = 4; //id mặc định do chưa có data
            L_Invoice = db.Invoices.Where(x => x.CustomerId == userID).ToList();
           foreach(Invoice invoice in L_Invoice)
            {
                L_Invoice_Detail = db.InvoiceDetails.Where(x => x.InvoiceId == invoice.Id).ToList();
                foreach (InvoiceDetail invoicedt in L_Invoice_Detail)
                {
                    Order order = new Order();
                    Product product = db.Products.Where(x => x.Id == invoicedt.ProductId).First();
                    order.Image = product.Image;
                    order.VendorName = db.Vendors.Where(x => x.Id == product.VendorId).First().Name;
                    order.ProductName = product.Name;
                    order.BrandName = db.Brands.Where(x => x.Id == product.BrandId).First().Name;
                    order.Quantity = invoicedt.Quantity;
                     if (product.DiscountRate != 0)
                        order.PriceOrg = $"{ (product.Price):0,0 đ}";
                     else
                        order.PriceOrg = "";
                    order.TotalPrice = $"{(invoicedt.Cost):0,0 đ}";
                    order.PriceDisc = $"{((long)invoicedt.Cost / order.Quantity):0,0 đ}"; 
                    order.Status = db.InvoiceStatuses.Where(x => x.Id == invoice.StatusId).First().Name;
                    l_Order.Add(order);
                    AddtoCart = new RelayCommand<object>((p) => { return true; }, (p) => { AddtoCartExec(userID,product.Id); });
                }
            }

        }
        void AddtoCartExec(int userID, int productID)
        {
            GoninDigitalDBContext db = DataProvider.Instance.Db;
            if (db.Carts.Where(x => x.UserId == userID & x.ProductId == productID).Count() == 0) 
            {
                Cart cart = new Cart();
                cart.UserId = userID;
                cart.ProductId = productID; 
                cart.Quantity = 1;
                db.Carts.Add(cart);
                db.SaveChanges();
            }
            else
            {
                db.Carts.Where(x => x.UserId == userID & x.ProductId == productID).First().Quantity += 1; 
                db.SaveChanges();
            }
            MessageBox.Show("This product has been added to your cart");
        }
    }
}
