using GoninDigital.Models;
using GoninDigital.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Page = ModernWpf.Controls.Page;

namespace GoninDigital.Views.DashBoardPages
{
    /// <summary>
    /// Interaction logic for CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        public CartPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            (DataContext as CartPageViewModel).OnNavigatedTo();
        }

        private void lvCartItemGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var items = (System.Collections.IList)lvCartItemGroup.SelectedItems;

            (DataContext as CartPageViewModel).SelectedProducts = items.Cast<Cart>();
        }

        private void NumberBox_ValueChanged(ModernWpf.Controls.NumberBox sender, ModernWpf.Controls.NumberBoxValueChangedEventArgs args)
        {
            (DataContext as CartPageViewModel).SelectedProducts = (DataContext as CartPageViewModel).SelectedProducts;
            (DataContext as CartPageViewModel).Update();
        }
    }
}
