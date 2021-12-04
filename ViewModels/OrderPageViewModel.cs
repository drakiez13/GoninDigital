using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using GoninDigital.Models;
using GoninDigital.SharedControl;
using System.Windows;
using System.Windows.Input;
using GoninDigital.Models;
using GoninDigital.Properties;
using GoninDigital.Views;
using ModernWpf.Controls;
using System.Globalization;
using GoninDigital.Views;
using GoninDigital.Views.DashBoardPages;
using Microsoft.EntityFrameworkCore;
using GoninDigital.Views.SharedPages;
using ModernWpf.Controls;

namespace GoninDigital.ViewModels
{
    class OrderPageViewModel : BaseViewModel
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
        private string flagImage;
        public string FlagImage
        {
            get { return flagImage; }
            set { flagImage = value; OnPropertyChanged(); }
        }
        private string flagImage1;
        public string FlagImage1
        {
            get { return flagImage1; }
            set { flagImage1 = value; OnPropertyChanged(); }
        }
        private string flagImage2;
        public string FlagImage2
        {
            get { return flagImage2; }
            set { flagImage2 = value; OnPropertyChanged(); }
        }
        private string flagImage3;
        public string FlagImage3
        {
            get { return flagImage3; }
            set { flagImage3 = value; OnPropertyChanged(); }
        }
        private string flagImage4;
        public string FlagImage4
        {
            get { return flagImage4; }
            set { flagImage4 = value; OnPropertyChanged(); }
        }
        private string image;
        public string Image
        {
            get { return image; }
            set { image = value; OnPropertyChanged(); }
        }
        private int userID;
        public int UserID
        {
            get { return userID; }
            set { userID = value; OnPropertyChanged(); }
        }
        #endregion
        #region Constructor
        public OrderPageViewModel()
        {
            Image = "/Resources/Images/NoOrderYet.jpg";
            //int userID = db.Users.Where(x => x.UserName == Settings.Default.usrname).First().Id;
            UserID = 4; //id mặc định do chưa có data
        }
        #endregion
        #region Private Methods
        public void OnNavigatedTo()
        {
            Load_HistoryPurchase();
        }
        private void Load_HistoryPurchase()
        {
            using (var db = new GoninDigitalDBContext())
            {
                UserID = 4;
                L_Order_Created = new List<Order>();
                L_Order_Accepted = new List<Order>();
                L_Order_Refused = new List<Order>();
                L_Order_Delivered = new List<Order>();
                L_Order_Canceled = new List<Order>();
                L_Invoice = db.Invoices.Where(x => x.CustomerId == UserID).ToList();
                foreach (Invoice invoice in L_Invoice)
                {
                    L_Invoice_Detail = db.InvoiceDetails.Where(x => x.InvoiceId == invoice.Id).ToList();
                    foreach (InvoiceDetail invoicedt in L_Invoice_Detail)
                    {
                        Order order = new Order();
                        Product product = db.Products.Where(x => x.Id == invoicedt.ProductId).First();
                        order.InvoiceId = invoice.Id;
                        order.Image = product.Image;
                        order.VendorName = db.Vendors.Where(x => x.Id == product.VendorId).First().Name;
                        order.ProductName = product.Name;
                        order.BrandName = db.Brands.Where(x => x.Id == product.BrandId).First().Name;
                        order.Quantity = invoicedt.Quantity;
                        if (product.Price != product.OriginPrice)
                            order.PriceOrg = $"{ (product.OriginPrice):0,0 đ}";
                        else
                            order.PriceOrg = "";
                        order.TotalPrice = $"{(invoicedt.Cost):0,0 đ}";
                        order.PriceDisc = $"{(product.Price):0,0 đ}  ";
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
                    }
                }
            }
            CheckItem();
        }
        private void CheckItem()
        {
            if (L_Order_Created.Count == 0)
                FlagImage = "Visible";
            else
            {
                FlagImage = "Hidden";
                L_Order_Created.Reverse();
            }
            if (L_Order_Accepted.Count == 0)
                FlagImage1 = "Visible";
            else
            {
                FlagImage1 = "Hidden";
                L_Order_Accepted.Reverse();
            }
            if (L_Order_Refused.Count == 0)
                FlagImage2 = "Visible";
            else
            {
                FlagImage2 = "Hidden";
                L_Order_Refused.Reverse();
            }
            if (L_Order_Delivered.Count == 0)
                FlagImage3 = "Visible";
            else
            {
                FlagImage3 = "Hidden";
                L_Order_Delivered.Reverse();
            }
                
            if (L_Order_Canceled.Count == 0)
                FlagImage4 = "Visible";
            else
            {
                FlagImage4 = "Hidden";
                l_Order_Canceled.Reverse();
            }   
        }
        #endregion
    }
}
