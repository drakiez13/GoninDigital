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
using GoninDigital.SharedControl;

namespace GoninDigital.Views.AdminPages
{
    /// <summary>
    /// Interaction logic for AdsPage.xaml
    /// </summary>
    public partial class AdsPage 
    {
        public AdsPage()
        {
            InitializeComponent();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (DataContext as AdsPageViewModel).Load_Ads();
        }

        private void AutoSuggestBox_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            (DataContext as AdsPageViewModel).SearchChanged();
        }

        private void AutoSuggestBox_QuerySubmitted(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            (DataContext as AdsPageViewModel).SearchAd();
        }

    }
}
