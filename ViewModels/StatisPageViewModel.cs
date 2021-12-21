using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using GoninDigital.Models;
using GoninDigital.Properties;
using GoninDigital.Utils;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;
using ModernWpf.Controls;

namespace GoninDigital.ViewModels
{
    internal class StatisPageViewModel : BaseViewModel
    {
        // Biểu đồ tròn
        private SeriesCollection _pie_SeriesCollection;
        public SeriesCollection pie_SeriesCollection
        {
            get { return _pie_SeriesCollection; }
            set { _pie_SeriesCollection = value; OnPropertyChanged(); }
        }


        // Biểu đồ cột ngang
        public SeriesCollection top5_SeriesCollection { get; set; }
        public string[] top5_Labels { get; set; }
        public Func<double, string> top5_Formatter { get; set; }

        // Biểu đồ đường
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }


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
        public ICommand optionchangedCommand { get; set; }
        public StatisPageViewModel()
        {
            Load_HistoryPurchase();
            Load_Revenue();
            Load_Top5BestSeller();

            // Biểu đồ tròn
            pie_SeriesCollection = new SeriesCollection()
                {
                    new PieSeries
                    {
                        Title = "Completed",
                        Values = new ChartValues<double> { deliveredInvoices.Count },
                        LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation)
                    },
                    new PieSeries
                    {
                        Title = "Cancelled",
                        Values = new ChartValues<double> { canceledInvoices.Count },
                        LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation)
                    },
                    new PieSeries
                    {
                        Title = "Refused",
                        Values = new ChartValues<double> { refusedInvoices.Count },
                        LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation)
                    }
                };

            // biểu đồ ngang
            top5_SeriesCollection = new SeriesCollection
            {
                new RowSeries
                {
                    Title = "2021",
                    Values = new ChartValues<double> { 10, 50, 39, 50 }
                }
            };

            top5_Labels = new[] { "Maria", "Susan", "Charles", "Frida" };
            top5_Formatter = value => value.ToString("N");

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

            optionchangedCommand = new RelayCommand<RadioButtons>((p) => { return true; }, (p) => { optionchangedCommandExcute(p); });
        }
        private void optionchangedCommandExcute(RadioButtons p)
        {
            if (p.SelectedIndex == 0)
            {
                // Biểu đồ tròn
                pie_SeriesCollection = new SeriesCollection()
                {
                    new PieSeries
                    {
                        Title = "Đã giao",
                        Values = new ChartValues<double> { deliveredInvoices.Count },
                        LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation)
                    },
                    new PieSeries
                    {
                        Title = "Đã hủy",
                        Values = new ChartValues<double> { canceledInvoices.Count },
                        LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation)
                    },
                    new PieSeries
                    {
                        Title = "Đã từ chối",
                        Values = new ChartValues<double> { refusedInvoices.Count },
                        LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation)
                    }
                };

                // biểu đồ ngang
                top5_SeriesCollection = new SeriesCollection
                {
                    new RowSeries
                    {
                        Title = "2015",
                        Values = new ChartValues<double> { 10, 50, 39, 50 }
                    }
                };

                top5_Labels = new[] { "Maria", "Susan", "Charles", "Frida" };
                top5_Formatter = value => value.ToString("N");

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
            else
            {
                // Biểu đồ tròn
                pie_SeriesCollection = new SeriesCollection()
                {
                    new PieSeries
                    {
                        Title = "Completed",
                        Values = new ChartValues<double> { deliveredInvoices.Count },
                        LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation)
                    },
                    new PieSeries
                    {
                        Title = "Cancelled",
                        Values = new ChartValues<double> { canceledInvoices.Count },
                        LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation)
                    },
                    new PieSeries
                    {
                        Title = "Refused",
                        Values = new ChartValues<double> { refusedInvoices.Count },
                        LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation)
                    }
                };

                // biểu đồ ngang
                top5_SeriesCollection = new SeriesCollection
                {
                    new RowSeries
                    {
                        Title = "2015",
                        Values = new ChartValues<double> { 10, 50, 39, 50 }
                    }
                };

                top5_Labels = new[] { "Maria", "Susan", "Charles", "Frida" };
                top5_Formatter = value => value.ToString("N");

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
        }

        private void Load_HistoryPurchase()
        {
            deliveredInvoices = new ObservableCollection<Invoice>();
            canceledInvoices = new ObservableCollection<Invoice>();
            refusedInvoices = new ObservableCollection<Invoice>();

            using (var db = new GoninDigitalDBContext())
            {
                var userInvoices = db.Invoices.Include(o => o.Customer)
                                              .Include(o => o.Vendor)
                                              .Include(o => o.InvoiceDetails).ThenInclude(o => o.Product)
                                              .Where(o => o.Customer.UserName == Settings.Default.usrname)
                                              .ToList();

                DeliveredInvoices = new ObservableCollection<Invoice>(userInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.DELIVERED));
                CanceledInvoices = new ObservableCollection<Invoice>(userInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.CANCELED));
                RefusedInvoices = new ObservableCollection<Invoice>(userInvoices.Where(o => o.StatusId == (int)Utils.Constants.InvoiceStatus.REFUSED));

            }
        }

        private void Load_Revenue()
        {
            using (var db = new GoninDigitalDBContext())
            {
                int thisVendorId = db.Vendors.Include(o => o.Owner)
                                             .Where(o => o.Owner.UserName == Settings.Default.usrname)
                                             .Single().Id;

                var perDay = db.Invoices.Where(o => o.VendorId == thisVendorId
                                          && o.StatusId == (int)Constants.InvoiceStatus.DELIVERED
                                          && o.FinishedAt.Value.Month == DateTime.Now.Month
                                          && o.FinishedAt.Value.Year == DateTime.Now.Year)
                                   .GroupBy(o => o.FinishedAt.Value.Day)
                                   .Select(o => new
                                   {
                                       o.Key,
                                       sum = o.Sum(x => x.Value),
                                   })
                                   .OrderBy(o => o.Key)
                                   .ToList();

                var perMonth = db.Invoices.Where(o => o.VendorId == thisVendorId
                                          && o.StatusId == (int)Constants.InvoiceStatus.DELIVERED
                                          && o.FinishedAt.Value.Year == DateTime.Now.Year)
                                   .GroupBy(o => o.FinishedAt.Value.Month)
                                   .Select(o => new
                                   {
                                       o.Key,
                                       sum = o.Sum(x => x.Value),
                                   })
                                   .OrderBy(o => o.Key)
                                   .ToList();
            }
        }
        private void Load_Top5BestSeller()
        {
            using (var db = new GoninDigitalDBContext())
            {
                int thisVendorId = db.Vendors.Include(o => o.Owner)
                                             .Where(o => o.Owner.UserName == Settings.Default.usrname)
                                             .Single().Id;

                List<Product> allTime;
                var fetchedProducts = db.Vendors.Include(o => o.Products)
                                                .Where(o => o.Id == thisVendorId)
                                                .Single().Products
                                                .OrderByDescending(o => o.Buy).ToList();
                if (fetchedProducts.Count >= 5)
                    allTime = fetchedProducts.Take(5).ToList();
                else
                    allTime = fetchedProducts;

                List<Product> byMonth;
                var fetched = db.InvoiceDetails.Include(o => o.Invoice)
                                               .Include(o => o.Product)
                                               .Where(o => o.Invoice.VendorId == thisVendorId
                                                     && o.Invoice.StatusId == (int)Constants.InvoiceStatus.DELIVERED
                                                     && DateTime.Now.Month == o.Invoice.FinishedAt.Value.Month
                                                     && DateTime.Now.Year == o.Invoice.FinishedAt.Value.Year)
                                               .GroupBy(o => o.ProductId)
                                               .Select(o => new
                                               {
                                                   o.Key,
                                                   sum = o.Sum(x => x.Quantity)
                                               })
                                               .OrderByDescending(o => o.sum)
                                               .Select(o => o.Key)
                                               .ToList();
                var fetchedProductsMonth = db.Products.Where(o => fetched.Contains(o.Id)).ToList();

                if (fetchedProductsMonth.Count >= 5)
                    byMonth = fetchedProductsMonth.Take(5).ToList();
                else
                    byMonth = fetchedProductsMonth;

            }
        }
    }
}
