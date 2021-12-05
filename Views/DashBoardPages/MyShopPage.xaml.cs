using GoninDigital.Models;
using GoninDigital.ViewModels;
using GoninDigital.Views.DashBoardPages.MyShopPages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Page = ModernWpf.Controls.Page;

namespace GoninDigital.Views.DashBoardPages
{
    /// <summary>
    /// Interaction logic for MyShopPage.xaml
    /// </summary>
    public partial class MyShopPage : Page
    {
        private Dictionary<string, Page> pages;
        
        public MyShopPage()
        {
            InitializeComponent();
            (DataContext as MyShopViewModel).IsOwner = true;
            pages = new Dictionary<string, Page>();
        }
        public MyShopPage(int vendorId)
        {
            InitializeComponent();
            pages = new Dictionary<string, Page>();
            (DataContext as MyShopViewModel).IsOwner = false;
            (DataContext as MyShopViewModel).VisibilityOwner = "Visible";
            using (var db = new GoninDigitalDBContext())
            {
                (DataContext as MyShopViewModel).Vendor = db.Vendors
                    .Include(o => o.Products).First(o => o.Id == vendorId);
                db.ProductCategories.ToList();
                (DataContext as MyShopViewModel).Products = new ObservableCollection<Product>(
                    (DataContext as MyShopViewModel).Vendor.Products
                    );
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            (DataContext as MyShopViewModel).OnNavigatedTo();
        }

        private void nav_SelectionChanged(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                frame.Navigate(typeof(HomePage));
                return;
            }
            var selectedItem = (ModernWpf.Controls.NavigationViewItem)args.SelectedItem;
            if (selectedItem != null)
            {
                string selectedItemTag = (string)selectedItem.Tag;
                string pageName = "GoninDigital.Views.DashBoardPages.MyShopPages." + selectedItemTag;

                Page togo;
                if (!pages.TryGetValue(pageName, out togo))
                {
                    Type pageType = typeof(HomeTab).Assembly.GetType(pageName);
                    togo = (Page)Activator.CreateInstance(pageType);
                    pages.Add(pageName, togo);
                }
                frame.Navigate(togo);
            }
            else
            {
                frame.Navigate(typeof(HomeTab));
            }
        }
    }
}
