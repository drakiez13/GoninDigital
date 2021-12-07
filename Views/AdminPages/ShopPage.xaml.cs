using System;
using System.Collections.Generic;
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
using GoninDigital.ViewModels;
using GoninDigital.Models;

namespace GoninDigital.Views.AdminPages
{
    /// <summary>
    /// Interaction logic for ShopPage.xaml
    /// </summary>
    public partial class ShopPage : Page
    {
        public ShopPage()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vendors = (System.Collections.IList)lvShopPage.SelectedItems;

            (DataContext as ManageShopPageViewModel).SelectedVendors = vendors.Cast<Vendor>();
        }
    }
}
