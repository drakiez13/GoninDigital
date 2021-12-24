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
    /// Interaction logic for CategoryManagePage.xaml
    /// </summary>
    public partial class CategoryManagePage : Page
    {
        public CategoryManagePage()
        {
            InitializeComponent();
        }

        private void AutoSuggestBox_TextChanged(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxTextChangedEventArgs args)
        {
            (DataContext as ManageCategoryViewModel).SearchChanged();
        }

        private void AutoSuggestBox_QuerySubmitted(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            (DataContext as ManageCategoryViewModel).SearchCategory();
        }

        private void AutoSuggestBox_QuerySubmitted_1(ModernWpf.Controls.AutoSuggestBox sender, ModernWpf.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            (DataContext as ManageCategoryViewModel).AddCategory();
        }
    }
}
