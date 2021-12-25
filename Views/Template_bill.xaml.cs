using GoninDigital.Models;
using GoninDigital.Properties;
using GoninDigital.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GoninDigital.Views
{
    /// <summary>
    /// Interaction logic for Template_bill.xaml
    /// </summary>
    public partial class Template_bill : Window
    {

        public Template_bill(Invoice x)
        {
            InitializeComponent();
            DataContext = new export_bill(x);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "invoice");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
        
    }
    internal class export_bill : BaseViewModel
    {
        private ObservableCollection<Invoice> deliveredInvoices;
        public ObservableCollection<Invoice> DeliveredInvoices
        {
            get { return deliveredInvoices; }
            set { deliveredInvoices = value; OnPropertyChanged(); }
        }
        private Invoice invoiceCurrent;
        public Invoice InvoiceCurrent
        {
            get { return invoiceCurrent; }
            set { invoiceCurrent = value; OnPropertyChanged();}
        }
        private User customer;
        public User Customer
        {
            get { return customer; }
            set { customer = value; OnPropertyChanged(); }
        }
        public export_bill(Invoice x)
        {
            Load(x);
        }
        private async void Load(Invoice x)
        {
            DeliveredInvoices = new ObservableCollection<Invoice> { x };
            Customer = x.Customer;
            InvoiceCurrent = x;
        }
    }
}
