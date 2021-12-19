using GoninDigital.Models;
using GoninDigital.ViewModels;
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

namespace GoninDigital.Views.AdminPages
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductsManagerPage : Page
    {
        public ProductsManagerPage()
        {
            InitializeComponent();
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var products = (System.Collections.IList)lvProductPage.SelectedItems;

            (DataContext as ManageProductPageViewModel).SelectedProducts = products.Cast<Product>();
        }

        private void AutoSuggestBox_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            (DataContext as ManageProductPageViewModel).SearchChanged(toggleSwitch.IsOn);
        }

        private void AutoSuggestBox_QuerySubmitted(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            (DataContext as ManageProductPageViewModel).SearchProduct(toggleSwitch.IsOn);
        }

        private void toggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            (DataContext as ManageProductPageViewModel).ToogleChanged(toggleSwitch.IsOn);
        }
    }
}
