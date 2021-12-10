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
        public string ImageEmpty { get; set; }
        public ICommand CancelInvoice { get; set; }
        public ICommand ReOrderInvoice { get; set; }

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
                o.StatusId = (int)Constants.InvoiceStatus.ACCEPTED;
                o.CreatedAt = System.DateTime.Now;
                o.FinishedAt=null;
                CanceledInvoices.Remove(o);
                CreatedInvoices.Add(o);
                using (var db = new GoninDigitalDBContext())
                {
                    db.Update(o);
                    db.SaveChanges();
                }
            });
            ImageEmpty = "/Resources/Images/NoOrderYet.jpg";
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
                                              .Where(o => o.Customer.Id==4)
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
