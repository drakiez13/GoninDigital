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
using Page = ModernWpf.Controls.Page;

namespace GoninDigital.Views.DashBoardPages
{
    /// <summary>
    /// Interaction logic for MyShopPage.xaml
    /// </summary>
    public partial class MyShopPage : Page
    {
        public MyShopPage()
        {
            InitializeComponent();
            (DataContext as MyShopViewModel).IsOwner=true;
        }
        public MyShopPage(Vendor vendor)
        {
            InitializeComponent();
            (DataContext as MyShopViewModel).IsOwner = false;
            (DataContext as MyShopViewModel).Vendor = vendor;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            (DataContext as MyShopViewModel).OnNavigatedTo();
        }
    }
}
