using GoninDigital.Models;
using GoninDigital.Properties;
using GoninDigital.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Page = ModernWpf.Controls.Page;

namespace GoninDigital.Views.SharedPages
{
    /// <summary>
    /// Interaction logic for CartPage_Purchase.xaml
    /// </summary>
    public partial class CheckoutPage : Page
    {
        public Action<object> OnSuccess { get; set; }
        public Action<object> OnFailure { get; set; }
        public ObservableCollection<Cart> Products { get; set; }
        public ICommand OrderButton { get; set; }
        public ICommand CancelButton { get; set; }
        public User User { get; set; }

        void Init()
        {
            using (var db = new GoninDigitalDBContext())
            {
                User = db.Users.Single(o => o.UserName == Settings.Default.usrname);
            }

            OrderButton = new RelayCommand<object>(o => true, o => {
                var data = Products.GroupBy(o => o.Product.VendorId);

                List<Invoice> invoices = new List<Invoice>(data.Count());

                data.ToList().ForEach(o => {
                    var dataOneVendor = o.ToList();
                    long invoiceValue = 0;
                    var invoiceDetails = new List<InvoiceDetail>();
                    Invoice invoice = new Invoice
                    {
                        StatusId = 1,
                        CustomerId = User.Id,
                        VendorId = dataOneVendor[0].Product.VendorId,
                        CreatedAt = DateTime.Now,
                    };
                    dataOneVendor.ForEach(o => {
                        invoiceValue += o.Quantity * o.Product.Price;
                        invoiceDetails.Add(new InvoiceDetail { 
                            ProductId = o.ProductId,
                            Quantity = o.Quantity,
                            Cost = o.Quantity * o.Product.Price,
                        });
                    });

                    invoice.InvoiceDetails = invoiceDetails;
                    invoice.Value = invoiceValue;
                    invoices.Add(invoice);
                });

                try
                {
                    using (var db = new GoninDigitalDBContext())
                    {
                        db.Invoices.AddRange(invoices);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    OnFailure?.Invoke(this);
                }
                

                OnSuccess?.Invoke(this);
                DashBoard.RootFrame.GoBack();
            });

            CancelButton = new RelayCommand<object>(o => true, o =>
            {
                OnFailure?.Invoke(this);
                DashBoard.RootFrame.GoBack();
            });
        }

        public CheckoutPage(IEnumerable<Cart> products, Action<object> onSuccess = null, Action<object> onFailure = null)
        {
            this.OnSuccess = onSuccess;
            this.Products = new ObservableCollection<Cart>(products);
            Init();
            InitializeComponent();
            
            
        }

        public CheckoutPage(Product product, int quantity, Action<object> onSuccess = null, Action<object> onFailure = null)
        {
            var tmp = new List<Cart>();
            tmp.Add(new Cart { ProductId = product.Id, Quantity = quantity, Product = product });
            this.Products = new ObservableCollection<Cart>(tmp);
            this.OnSuccess = onSuccess;
            Init();
            InitializeComponent();
        }
    }
}
