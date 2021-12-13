using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GoninDigital.Models;
using GoninDigital.ViewModels;
using GoninDigital.Views;
using GoninDigital.Views.SharedPages;
using static GoninDigital.Views.SharedPages.ProductListPage;

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
            new Product { Name="Loading...", Price=100000},
            new Product { Name="Loading...", Price=200000},
            new Product { Name="Loading...", Price=300000},
            new Product { Name="Loading...", Price=400000},
            new Product { Name="Loading...", Price=500000}
        };

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(object), typeof(ProductCardGroup2), new PropertyMetadata("Title"));
        public static readonly DependencyProperty SubtitleProperty =
            DependencyProperty.Register("Subtitle", typeof(object), typeof(ProductCardGroup2), new PropertyMetadata("Subtitle"));
        public static readonly DependencyProperty ProductListProperty =
            DependencyProperty.Register("ProductList", typeof(object), typeof(ProductCardGroup2), new PropertyMetadata(metaProducts), o => o != null);
        public static readonly DependencyProperty GroupBackgroundProperty =
            DependencyProperty.Register("GroupBackground", typeof(object), typeof(ProductCardGroup2), new PropertyMetadata("DarkSeaGreen"), o => o != null);
        
        public ICommand OnSeeAllClick { get; set; }

        public ProductCardGroup2()
        {
            OnSeeAllClick = new RelayCommand<object>(o => true, o =>
            {
                var parameters = new Parameter
                {
                    title = (string)Title,
                    subtitle = (string)Subtitle,
                    products = ProductList as List<Product>,
                    cover = (string)GroupBackground
                };

                DashBoard.RootFrame.Navigate(new ProductListPage(parameters));
            });
            InitializeComponent();
        }
    }
}
