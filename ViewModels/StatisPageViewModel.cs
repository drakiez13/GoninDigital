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
    }
}
