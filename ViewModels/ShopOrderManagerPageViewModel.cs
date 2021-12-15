using GoninDigital.Models;
using GoninDigital.Properties;
using GoninDigital.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GoninDigital.ViewModels
{
    class ShopOrderManagerPageViewModel : BaseViewModel
    {
        private ObservableCollection<Invoice> createdInvoices;
        public ObservableCollection<Invoice> CreatedInvoices
        {
            get { return createdInvoices; }
            set { createdInvoices = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Invoice> acceptedInvoices;
        public ObservableCollection<Invoice> AcceptedInvoices
        {
            get { return acceptedInvoices; }
            set { acceptedInvoices = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Invoice> deliveredInvoices;
        public ObservableCollection<Invoice> DeliveredInvoices
        {
            get { return deliveredInvoices; }
            set { deliveredInvoices = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Invoice> canceledInvoices;
        public ObservableCollection<Invoice> CanceledInvoices
        {
            get { return canceledInvoices; }
            set { canceledInvoices = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Invoice> refusedInvoices;
        public ObservableCollection<Invoice> RefusedInvoices
        {
            get { return refusedInvoices; }
            set { refusedInvoices = value; OnPropertyChanged(); }
        }

        public ICommand CancelInvoice { get; set; }
        public ICommand ReOrderInvoice { get; set; }

        public ShopOrderManagerPageViewModel()
        {
            createdInvoices = new ObservableCollection<Invoice>();
            acceptedInvoices = new ObservableCollection<Invoice>();
            deliveredInvoices = new ObservableCollection<Invoice>();
            canceledInvoices = new ObservableCollection<Invoice>();
            refusedInvoices = new ObservableCollection<Invoice>();

            CancelInvoice = new RelayCommand<Invoice>(o => true, o => {
                o.StatusId = (int)Constants.InvoiceStatus.CANCELED;
                o.FinishedAt = System.DateTime.Now;
                CreatedInvoices.Remove(o);
                CanceledInvoices.Add(o);
                using (var db = new GoninDigitalDBContext())
                {
                    db.Update(o);
                    db.SaveChanges();
                }
            });
            ReOrderInvoice = new RelayCommand<Invoice>(o => true, o => {
                o.StatusId = (int)Constants.InvoiceStatus.ACCEPTED;
                o.CreatedAt = System.DateTime.Now;
                o.FinishedAt = null;
                CanceledInvoices.Remove(o);
                CreatedInvoices.Add(o);
                using (var db = new GoninDigitalDBContext())
                {
                    db.Update(o);
                    db.SaveChanges();
                }
            });
        }

        public void OnNavigatedTo()
        {
            Load_HistoryPurchase();
        }
        private void Load_HistoryPurchase()
        {

            using (var db = new GoninDigitalDBContext())
            {
                var vendor = db.Vendors.Include(o => o.Owner)
                               .First(o => o.Owner.UserName == Settings.Default.usrname);
                var vendorInvoices = db.Invoices.Include(o => o.Customer)
                                              .Include(o => o.Vendor)
                                              .Include(o => o.InvoiceDetails).ThenInclude(o => o.Product)
                                              .Where(o => o.Vendor.Equals(vendor))
                                              .ToList();
                CreatedInvoices = new ObservableCollection<Invoice>(vendorInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.CREATED));
                AcceptedInvoices = new ObservableCollection<Invoice>(vendorInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.ACCEPTED));
                DeliveredInvoices = new ObservableCollection<Invoice>(vendorInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.DELIVERED));
                CanceledInvoices = new ObservableCollection<Invoice>(vendorInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.CANCELED));
                RefusedInvoices = new ObservableCollection<Invoice>(vendorInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.REFUSED));

            }
        }
    }
}
