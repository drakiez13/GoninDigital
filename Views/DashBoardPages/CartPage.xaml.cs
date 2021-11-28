using GoninDigital.ViewModels;
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
    }
}
