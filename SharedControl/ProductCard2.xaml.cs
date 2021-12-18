using GoninDigital.Models;
using GoninDigital.Views;
using GoninDigital.Views.SharedPages;
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
    /// Interaction logic for ProductCard2.xaml
    /// </summary>
    public partial class ProductCard2 : UserControl
    {
        public Product ProductInfo
        {
            get => (Product)GetValue(ProductProperty);
            set => SetValue(ProductProperty, value);
        }
        public bool Clickable
        {
            get => (bool)GetValue(ClickableProperty);
            set => SetValue(ClickableProperty, value);
        }

        public static readonly DependencyProperty ProductProperty =
            DependencyProperty.Register("ProductInfo", typeof(Product), typeof(ProductCard2), new PropertyMetadata(new Product()));
        public static readonly DependencyProperty ClickableProperty =
            DependencyProperty.Register("Clickable", typeof(bool), typeof(ProductCard2), new PropertyMetadata(true));

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductInfo != null && Clickable)
            {
                DashBoard.RootFrame.Navigate(new ProductPage(ProductInfo));
            }
        }
        public ProductCard2()
        {
            InitializeComponent();
        }
    }
}
