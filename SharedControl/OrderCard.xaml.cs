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
    /// Interaction logic for OrderCard.xaml
    /// </summary>
    public partial class OrderCard : UserControl
    {
        public object Image
        {
            get => (object)GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }
        public object VendorName
        {
            get => (object)GetValue(VendorNameProperty);
            set => SetValue(VendorNameProperty, value);
        }
        public object ProductName
        {
            get => (object)GetValue(ProductNameProperty);
            set => SetValue(ProductNameProperty, value);
        }
        public object BrandName
        {
            get => (object)GetValue(BrandNameProperty);
            set => SetValue(BrandNameProperty, value);
        }
        public object Quantity
        {
            get => (object)GetValue(QuantityProperty);
            set => SetValue(QuantityProperty, value);
        }
        public object PriceDisc
        {
            get => (object)GetValue(PriceDiscProperty);
            set => SetValue(PriceDiscProperty, value);
        }
        public object PriceOrg
        {
            get => (object)GetValue(PriceOrgProperty);
            set => SetValue(PriceOrgProperty, value);
        }
        public object Status
        {
            get => (object)GetValue(StatusProperty);
            set => SetValue(StatusProperty, value);
        }
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(object), typeof(CartItem), new PropertyMetadata("/Resources/Images/BlankImage.jpg"));
        public static readonly DependencyProperty VendorNameProperty =
            DependencyProperty.Register("VendorName", typeof(object), typeof(OrderCard), new PropertyMetadata(Unknown));
        public static readonly DependencyProperty ProductNameProperty =
            DependencyProperty.Register("ProductName", typeof(object), typeof(OrderCard), new PropertyMetadata(Unknown));
        public static readonly DependencyProperty BrandNameProperty =
            DependencyProperty.Register("BrandName", typeof(object), typeof(OrderCard), new PropertyMetadata(Unknown));
        public static readonly DependencyProperty QuantityProperty =
            DependencyProperty.Register("Quantity", typeof(object), typeof(OrderCard), new PropertyMetadata(0));
        public static readonly DependencyProperty PriceDiscProperty =
            DependencyProperty.Register("PriceDisc", typeof(object), typeof(OrderCard), new PropertyMetadata(0));
        public static readonly DependencyProperty PriceOrgProperty =
            DependencyProperty.Register("PriceOrg", typeof(object), typeof(OrderCard), new PropertyMetadata(0));
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register("Status", typeof(object), typeof(OrderCard), new PropertyMetadata(Unknown));
        public OrderCard()
        {
            InitializeComponent();
        }
    }
}
