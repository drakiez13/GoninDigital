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

namespace GoninDigital.SharedControl
{
    /// <summary>
    /// Interaction logic for ProductCardGroup2.xaml
    /// </summary>
    public partial class ProductCardGroup2 : UserControl
    {
        public object Title
        {
            get => (object)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public object Subtitle
        {
            get => (object)GetValue(SubtitleProperty);
            set => SetValue(SubtitleProperty, value);
        }
        public object OnSeeAllClick
        {
            get => (object)GetValue(OnSeeAllClickProperty);
            set => SetValue(OnSeeAllClickProperty, value);
        }
        public object ProductList
        {
            get => (object)GetValue(ProductListProperty);
            set => SetValue(ProductListProperty, value);
        }
        public object GroupBackground
        {
            get => (object)GetValue(GroupBackgroundProperty);
            set => SetValue(GroupBackgroundProperty, value);
        }

        private static List<Product> metaProducts = new List<Product> {
            new Product { Name="Product 1", Price=100000},
            new Product { Name="Product 2", Price=200000},
            new Product { Name="Product 3", Price=300000},
            new Product { Name="Product 4", Price=400000},
            new Product { Name="Product 5", Price=400000}
        };

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(object), typeof(ProductCardGroup2), new PropertyMetadata("Title"));
        public static readonly DependencyProperty SubtitleProperty =
            DependencyProperty.Register("Subtitle", typeof(object), typeof(ProductCardGroup2), new PropertyMetadata("Subtitle"));
        public static readonly DependencyProperty OnSeeAllClickProperty =
            DependencyProperty.Register("OnSeeAllClick", typeof(object), typeof(ProductCardGroup2), new PropertyMetadata(null));
        public static readonly DependencyProperty ProductListProperty =
            DependencyProperty.Register("ProductList", typeof(object), typeof(ProductCardGroup2), new PropertyMetadata(metaProducts));
        public static readonly DependencyProperty GroupBackgroundProperty =
            DependencyProperty.Register("GroupBackground", typeof(object), typeof(ProductCardGroup2), new PropertyMetadata("DarkSeaGreen"));
        public ProductCardGroup2()
        {
            InitializeComponent();
        }
    }
}
