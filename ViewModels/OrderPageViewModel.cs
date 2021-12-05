using System.Collections.Generic;
using System.Linq;
using GoninDigital.Models;
using GoninDigital.SharedControl;
using GoninDigital.Properties;
using Microsoft.EntityFrameworkCore;
using GoninDigital.Utils;

namespace GoninDigital.ViewModels
{
    class OrderPageViewModel : BaseViewModel
    {
        private List<Invoice> createdInvoices;
        public List<Invoice> CreatedInvoices
        {
            get { return createdInvoices; }
            set { createdInvoices = value; OnPropertyChanged(); }
        }
        private List<Invoice> acceptedInvoices;
        public List<Invoice> AcceptedInvoices
        {
            get { return acceptedInvoices; }
            set { acceptedInvoices = value; OnPropertyChanged(); }
        }
        private List<Invoice> deliveredInvoices;
        public List<Invoice> DeliveredInvoices
        {
            get { return deliveredInvoices; }
            set { deliveredInvoices = value; OnPropertyChanged(); }
        }
        private List<Invoice> canceledInvoices;
        public List<Invoice> CanceledInvoices
        {
            get { return canceledInvoices; }
            set { canceledInvoices = value; OnPropertyChanged(); }
        }
        private List<Invoice> refusedInvoices;
        public List<Invoice> RefusedInvoices
        {
            get { return refusedInvoices; }
            set { refusedInvoices = value; OnPropertyChanged(); }
        }

        public OrderPageViewModel()
        {
            createdInvoices = new List<Invoice>();
            acceptedInvoices = new List<Invoice>();
            deliveredInvoices = new List<Invoice>();
            canceledInvoices = new List<Invoice>();
            refusedInvoices = new List<Invoice>();
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
                                              .Where(o => o.Customer.UserName == Settings.Default.usrname)
                                              .ToList();
                CreatedInvoices = userInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.CREATED).ToList();
                AcceptedInvoices = userInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.ACCEPTED).ToList();
                DeliveredInvoices = userInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.DELIVERED).ToList();
                CanceledInvoices = userInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.CANCELED).ToList();
                RefusedInvoices = userInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.REFUSED).ToList();

            }
        }
    }
}
