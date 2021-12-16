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

        public ICommand RefuseCommand { get; set; }
        public ICommand AcceptCommand { get; set; }

        public ShopOrderManagerPageViewModel()
        {
            createdInvoices = new ObservableCollection<Invoice>();
            acceptedInvoices = new ObservableCollection<Invoice>();
            deliveredInvoices = new ObservableCollection<Invoice>();
            canceledInvoices = new ObservableCollection<Invoice>();
            refusedInvoices = new ObservableCollection<Invoice>();

            RefuseCommand = new RelayCommand<Invoice>(o => true, o => {
                o.StatusId = (int)Constants.InvoiceStatus.REFUSED;
                CreatedInvoices.Remove(o);
                RefusedInvoices.Add(o);
                using (var db = new GoninDigitalDBContext())
                {
                    db.Invoices.Update(o);
                    db.SaveChanges();
                }
            });
            AcceptCommand = new RelayCommand<Invoice>(o => true, o => {
                o.StatusId = (int)Constants.InvoiceStatus.ACCEPTED;
                CreatedInvoices.Remove(o);
                AcceptedInvoices.Add(o);
                using (var db = new GoninDigitalDBContext())
                {
                    db.Invoices.Update(o);
                    db.SaveChanges();
                }
            });
        }

        public void OnNavigatedTo()
        {
            Load();
        }
        private void Load()
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
