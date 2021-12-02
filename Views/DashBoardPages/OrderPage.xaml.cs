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
using GoninDigital.ViewModels;
using GoninDigital.Models;
using Page = ModernWpf.Controls.Page;

namespace GoninDigital.Views.DashBoardPages
{
    /// <summary>
    /// Interaction logic for HotDealPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            (DataContext as OrderPageViewModel).OnNavigatedTo();
            
        }
    }
}
