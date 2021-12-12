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
    public partial class ProductListPage : Page
    {
        public ObservableCollection<Product> Products { get; set; }
        public string PageTitle { get; set; }
        public string Subtitle { get; set; }
        public string Cover { get; set; }

        public ProductListPage(ObservableCollection<Product> products, string title, string subtitle, string cover = null)
        {
            Products = products;
            PageTitle = title;
            Subtitle = subtitle;
            Cover = cover;

            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            // Checkout page should not in back stack
            DashBoard.RootFrame.RemoveBackEntry();
        }
    }
}
