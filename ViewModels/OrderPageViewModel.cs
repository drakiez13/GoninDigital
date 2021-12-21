using System.Collections.Generic;
using System.Linq;
using GoninDigital.Models;
using GoninDigital.SharedControl;
using GoninDigital.Properties;
using Microsoft.EntityFrameworkCore;
using GoninDigital.Utils;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace GoninDigital.ViewModels
{
    class OrderPageViewModel : BaseViewModel
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
        public ICommand ReceiveCommand { get; set; }

        public OrderPageViewModel()
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
                if (o.StatusId == (int)Constants.InvoiceStatus.CANCELED)
                {
                    CanceledInvoices.Remove(o);
                }
                if (o.StatusId == (int)Constants.InvoiceStatus.REFUSED)
                {
                    RefusedInvoices.Remove(o);
                }
                if (o.StatusId == (int)Constants.InvoiceStatus.DELIVERED)
                {
                    DeliveredInvoices.Remove(o);
                }
                o.StatusId = (int)Constants.InvoiceStatus.CREATED;
                o.CreatedAt = System.DateTime.Now;
                o.FinishedAt=null;
                
                CreatedInvoices.Add(o);
                
                using (var db = new GoninDigitalDBContext())
                {
                    db.Update(o);
                    db.SaveChanges();
                }
            });
            ReceiveCommand = new RelayCommand<Invoice>(o => true, o => {
                o.StatusId = (int)Constants.InvoiceStatus.DELIVERED;
                o.FinishedAt = System.DateTime.Now;
                AcceptedInvoices.Remove(o);
                DeliveredInvoices.Add(o);
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
                var userInvoices = db.Invoices.Include(o => o.Customer)
                                              .Include(o => o.Vendor)
                                              .Include(o => o.InvoiceDetails).ThenInclude(o => o.Product)
                                              .Where(o => o.Customer.UserName == Settings.Default.usrname)
                                              .ToList();
                CreatedInvoices = new ObservableCollection<Invoice>(userInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.CREATED));
                AcceptedInvoices = new ObservableCollection<Invoice>(userInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.ACCEPTED));
                DeliveredInvoices = new ObservableCollection<Invoice>(userInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.DELIVERED));
                CanceledInvoices = new ObservableCollection<Invoice>(userInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.CANCELED));
                RefusedInvoices = new ObservableCollection<Invoice>(userInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.REFUSED));

            }
        }
    }
}
