using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using GoninDigital.Models;
using GoninDigital.Properties;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;

namespace GoninDigital.ViewModels
{
    internal class StatisPageViewModel : BaseViewModel
    {
        public StatisPageViewModel()
        {
            createdInvoices = new ObservableCollection<Invoice>();
            acceptedInvoices = new ObservableCollection<Invoice>();
            deliveredInvoices = new ObservableCollection<Invoice>();
            canceledInvoices = new ObservableCollection<Invoice>();
            refusedInvoices = new ObservableCollection<Invoice>();

            Load_HistoryPurchase();

            // Biểu đồ tròn
            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            // biểu đồ ngang
            Values1 = new List<string> { "1", "2", "3", "4", "5" };
            Values2 = new ChartValues<double> { 6, 2, 6, 3, 2};

            // Biểu đồ đường
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 40000000, 600000000, 5000000000, 200000000 ,40000000 }
                }
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            YFormatter = value => value.ToString("C", cul.NumberFormat);
        }
        public Func<ChartPoint, string> PointLabel { get; set; }
        public List<string> Values1 { get; set; }
        public ChartValues<double> Values2 { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }


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
