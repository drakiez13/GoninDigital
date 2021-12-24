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
using GoninDigital.Models;
using GoninDigital.ViewModels;

namespace GoninDigital.Views.AdminPages
{
    /// <summary>
    /// Interaction logic for BrandPage.xaml
    /// </summary>
    public partial class BrandsManagerPage
    {
        public BrandsManagerPage()
        {
            InitializeComponent();

        }

        private void AutoSuggestBox_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            (DataContext as BrandsViewModel).SearchChanged();
        }

        private void AutoSuggestBox_QuerySubmitted(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            (DataContext as BrandsViewModel).SearchBrand();
        }

        private void AutoSuggestBox_QuerySubmitted_1(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            (DataContext as BrandsViewModel).AddBrand();
        }
    }
}