using GoninDigital.Models;
using GoninDigital.Views;
using GoninDigital.Views.SharedPages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace GoninDigital.SharedControl
{
    /// <summary>
    /// Interaction logic for ProductCard.xaml
    /// </summary>
    public partial class ProductCard : UserControl
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
            DependencyProperty.Register("ProductInfo", typeof(Product), typeof(ProductCard), new PropertyMetadata(new Product()));
        public static readonly DependencyProperty ClickableProperty =
            DependencyProperty.Register("Clickable", typeof(bool), typeof(ProductCard), new PropertyMetadata(true));

        public ProductCard()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            if (ProductInfo != null && Clickable)
            {
                DashBoard.RootFrame.Navigate(new ProductPage(ProductInfo));
            }
        }
    }
}
