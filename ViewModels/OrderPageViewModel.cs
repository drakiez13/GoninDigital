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
using System.Globalization;

namespace GoninDigital.ViewModels
{
    class OrderPageViewModel: BaseViewModel
    {
        #region Properties
        private List<Order> l_Order_Created;
        public List<Order> L_Order_Created
        {
            get { return l_Order_Created; }
            set { l_Order_Created = value; OnPropertyChanged(); }
        }
        private List<Order> l_Order_Accepted;
        public List<Order> L_Order_Accepted
        {
            get { return l_Order_Accepted; }
            set { l_Order_Accepted = value; OnPropertyChanged(); }
        }
        private List<Order> l_Order_Refused;
        public List<Order> L_Order_Refused
        {
            get { return l_Order_Refused; }
            set { l_Order_Refused = value; OnPropertyChanged(); }
        }
        private List<Order> l_Order_Delivered;
        public List<Order> L_Order_Delivered
        {
            get { return l_Order_Delivered; }
            set { l_Order_Delivered = value; OnPropertyChanged(); }
        }
        private List<Order> l_Order_Canceled;
        public List<Order> L_Order_Canceled
        {
            get { return l_Order_Canceled; }
            set { l_Order_Canceled = value; OnPropertyChanged(); }
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
        GoninDigitalDBContext db = DataProvider.Instance.Db;
        #endregion
        #region Constructor
        public OrderPageViewModel()
        {
            L_Order_Created = new List<Order>();
            L_Order_Accepted = new List<Order>();
            L_Order_Refused = new List<Order>();
            L_Order_Delivered = new List<Order>();
            L_Order_Canceled = new List<Order>();
            //int userID = db.Users.Where(x => x.UserName == Settings.Default.usrname).First().Id;
            int userID = 4; //id mặc định do chưa có data
            Load_HistoryPurchase(userID);
        }
        #endregion
        #region Private Methods
        void Load_HistoryPurchase(int userID)
        {
            L_Invoice = db.Invoices.Where(x => x.CustomerId == userID).ToList();
            foreach (Invoice invoice in L_Invoice)
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
                    order.Date = invoice.CreatedAt.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                        switch (order.Status)
                    {
                        case "created   ":
                            L_Order_Created.Add(order);
                            break;
                        case "accepted   ":
                            L_Order_Accepted.Add(order);
                            break;
                        case "refused   ":
                            L_Order_Refused.Add(order);
                            break;
                        case "delivered   ":
                            L_Order_Delivered.Add(order);
                            break;
                        case "canceled   ":
                            L_Order_Canceled.Add(order);
                            break;
                    }
                    AddtoCart = new RelayCommand<object>((p) => { return true; }, (p) => { AddtoCartExec(userID, product.Id); });
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
        #endregion
    }
}
