using System;
using System.Collections;
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
        private SeriesCollection _top5_SeriesCollection;
        public SeriesCollection top5_SeriesCollection
        {
            get { return _top5_SeriesCollection; }
            set { _top5_SeriesCollection = value; OnPropertyChanged(); }
        }
        private string[] _top5_Labels;
        public string[] top5_Labels
        {
            get { return _top5_Labels; }
            set { _top5_Labels = value; OnPropertyChanged(); }
        }
        public Func<double, string> top5_Formatter { get; set; }

        // Biểu đồ đường
        private SeriesCollection _SeriesCollection;
        public SeriesCollection SeriesCollection
        {
            get { return _SeriesCollection; }
            set { _SeriesCollection = value; OnPropertyChanged(); }
        }
        private string[] _Labels;
        public string[] Labels
        {
            get { return _Labels; }
            set { _Labels = value; OnPropertyChanged(); }
        }
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

            // Biểu đồ tròn
            pie_SeriesCollection = new SeriesCollection()
                {
                    new PieSeries
                    {
                        DataLabels = true,
                        Title = "Completed",
                        Values = new ChartValues<double> { deliveredInvoices.Count },
                        LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation)
                    },
                    new PieSeries
                    {
                        DataLabels = true,
                        Title = "Cancelled",
                        Values = new ChartValues<double> { canceledInvoices.Count },
                        LabelPoint = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation)
                    },
                    new PieSeries
                    {
                        DataLabels = true,
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
                        Title = "Total Revenue",
                        //Values = new ChartValues<double> { 10, 50, 39, 50 }
                        Values = new ChartValues<int>(Load_Top5BestSeller(0).Select(r=>r.Buy).ToArray())
                    }
                };

            top5_Labels = Load_Top5BestSeller(0).Select(r => r.Name).ToArray();
            top5_Formatter = value => value.ToString("N");

            // Biểu đồ đường
            using (var db = new GoninDigitalDBContext())
            {
                int thisVendorId = db.Vendors.Include(o => o.Owner)
                                             .Where(o => o.Owner.UserName == Settings.Default.usrname)
                                             .Single().Id;

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

                SeriesCollection = new SeriesCollection
                {
                    new LineSeries
                    {

                        Values = new ChartValues<double> (perMonth.Select(r=>(double)r.sum).ToArray())
                    }
                };

                Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec" };
                CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
                YFormatter = value => value.ToString("C", cul.NumberFormat);
            }

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
                        Title = "Total Revenue",
                        //Values = new ChartValues<double> { 10, 50, 39, 50 }
                        Values = new ChartValues<int>(Load_Top5BestSeller(0).Select(r=>r.Buy).ToArray())
                    }
                };

                top5_Labels = Load_Top5BestSeller(0).Select(r => r.Name).ToArray();
                top5_Formatter = value => value.ToString("N");

                // Biểu đồ đường
                using (var db = new GoninDigitalDBContext())
                {
                    int thisVendorId = db.Vendors.Include(o => o.Owner)
                                                 .Where(o => o.Owner.UserName == Settings.Default.usrname)
                                                 .Single().Id;

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

                    SeriesCollection = new SeriesCollection
                    {
                        new LineSeries
                        {

                            Values = new ChartValues<double> (perMonth.Select(r=>(double)r.sum).ToArray())
                        }
                    };

                    Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec" };
                    CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
                    YFormatter = value => value.ToString("C", cul.NumberFormat);
                }
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
                        Title = "Total Revenue",
                        //Values = new ChartValues<double> { 10, 50, 39, 50 }
                        Values = new ChartValues<int>(Load_Top5BestSeller(1).Select(r=>r.Buy).ToArray())
                    }
                };

                top5_Labels = Load_Top5BestSeller(1).Select(r => r.Name).ToArray();
                top5_Formatter = value => value.ToString("N");

                // Biểu đồ đường
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

                    SeriesCollection = new SeriesCollection
                    {
                        new LineSeries
                        {

                            Values = new ChartValues<double> (perDay.Select(r=>(double)r.sum).ToArray())
                        }
                    };

                    Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" +
                        "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };
                    CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
                    YFormatter = value => value.ToString("C", cul.NumberFormat);
                }
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

        //private void Load_Revenue()
        //{
        //    using (var db = new GoninDigitalDBContext())
        //    {
        //        int thisVendorId = db.Vendors.Include(o => o.Owner)
        //                                     .Where(o => o.Owner.UserName == Settings.Default.usrname)
        //                                     .Single().Id;

        //        var perDay = db.Invoices.Where(o => o.VendorId == thisVendorId
        //                                  && o.StatusId == (int)Constants.InvoiceStatus.DELIVERED
        //                                  && o.FinishedAt.Value.Month == DateTime.Now.Month
        //                                  && o.FinishedAt.Value.Year == DateTime.Now.Year)
        //                           .GroupBy(o => o.FinishedAt.Value.Day)
        //                           .Select(o => new
        //                           {
        //                               o.Key,
        //                               sum = o.Sum(x => x.Value),
        //                           })
        //                           .OrderBy(o => o.Key)
        //                           .ToList();

        //        var perMonth = db.Invoices.Where(o => o.VendorId == thisVendorId
        //                                  && o.StatusId == (int)Constants.InvoiceStatus.DELIVERED
        //                                  && o.FinishedAt.Value.Year == DateTime.Now.Year)
        //                           .GroupBy(o => o.FinishedAt.Value.Month)
        //                           .Select(o => new
        //                           {
        //                               o.Key,
        //                               sum = o.Sum(x => x.Value),
        //                           })
        //                           .OrderBy(o => o.Key)
        //                           .ToList();

        //    }
        //}
        private List<Product> Load_Top5BestSeller(int choose_type)
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

                if (choose_type == 0)
                {
                    return allTime;
                }

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
                return byMonth;
            }
        }
    }
}
