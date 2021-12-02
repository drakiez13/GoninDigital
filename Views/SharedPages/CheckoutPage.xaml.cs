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
        ICommand OrderButton { get; set; }
        ICommand CancelButton { get; set; }
        public User User { get; set; }

        void Init()
        {
            using (var db = new GoninDigitalDBContext())
            {
                User = db.Users.Single(o => o.UserName == Settings.Default.usrname);
            }
            OrderButton = new RelayCommand<object>(o => true, o => {
                var data = Products.GroupBy(o => o.Product.VendorId);
                string tmp = "";
                data.ToList().ForEach(o => {
                    tmp += "==";
                    o.ToList().ForEach(o => tmp += o.Product.Name); ; });
                OnSuccess(this);
                DashBoard.RootFrame.GoBack();
            });

            CancelButton = new RelayCommand<object>(o => true, o =>
            {
                OnFailure(this);
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
            tmp.Add(new Cart { Product = product, Quantity = quantity });
            this.Products = new ObservableCollection<Cart>(tmp);
            this.OnSuccess = onSuccess;
            Init();
            InitializeComponent();
        }
    }
}
