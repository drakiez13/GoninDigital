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

namespace GoninDigital.SharedControl
{
    /// <summary>
    /// Interaction logic for AddProductDialog.xaml
    /// </summary>
    public partial class AddProductDialog : UserControl
    {
        public AddProductDialog()
        {
            InitializeComponent();
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            (this.DataContext as MyShopViewModel).UpdateCategorySpecDetails();
        }
    }
}
