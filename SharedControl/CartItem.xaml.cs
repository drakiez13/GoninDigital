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
    /// Interaction logic for CartItem.xaml
    /// </summary>
    public partial class CartItem : UserControl
    {
        public object Title
        {
            get => (object)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public object Image
        {
            get => (object)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }
        

        public object Price
        {
            get => (object)GetValue(PriceProperty);
            set => SetValue(PriceProperty, value);
        }
        public object TotalPrice
        {
            get => (object)GetValue(TotalPriceProperty);
            set => SetValue(TotalPriceProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(object), typeof(CartItem), new PropertyMetadata("Unknown"));
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(object), typeof(CartItem),
                new PropertyMetadata("/Resources/Images/BlankImage.jpg"));
      
        public static readonly DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(object), typeof(CartItem), new PropertyMetadata(0));
        public static readonly DependencyProperty TotalPriceProperty =
            DependencyProperty.Register("TotalPrice", typeof(object), typeof(CartItem), new PropertyMetadata(0));
        public CartItem()
        {
            InitializeComponent();
        }
    }
}
