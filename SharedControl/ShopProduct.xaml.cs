using GoninDigital.Models;
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
    /// Interaction logic for ShopProduct.xaml
    /// </summary>
    public partial class ShopProduct : UserControl
    {
        public object ProductList
        {
            get => (object)GetValue(ProductListProperty);
            set => SetValue(ProductListProperty, value);
        }
        private static List<Product> metaProducts = new List<Product> {
            new Product { Name="Product 1", Price=100000},
            new Product { Name="Product 2", Price=200000},
            new Product { Name="Product 3", Price=300000},
            new Product { Name="Product 4", Price=400000},
            new Product { Name="Product 5", Price=400000}
        };

        public static readonly DependencyProperty ProductListProperty =
            DependencyProperty.Register("ProductList", typeof(object), typeof(ShopProduct), new PropertyMetadata(metaProducts),(object o) => { return o != null;});
        public ShopProduct()
        {
            InitializeComponent();
        }
    }
}
