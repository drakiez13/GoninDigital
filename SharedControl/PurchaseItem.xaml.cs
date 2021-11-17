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
    /// Interaction logic for PurchaseItem.xaml
    /// </summary>
    public partial class PurchaseItem : UserControl
    {
        public object Title
        {
            get => (object)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public object Delivery
        {
            get => (object)GetValue(DeliveryProperty);
            set => SetValue(DeliveryProperty,value);
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
            DependencyProperty.Register("Title", typeof(object), typeof(PurchaseItem), new PropertyMetadata("Unknown"));

        public static readonly DependencyProperty DeliveryProperty =
            DependencyProperty.Register("Delivery", typeof(object), typeof(PurchaseItem), new PropertyMetadata(50.000));
        public static readonly DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(object), typeof(PurchaseItem), new PropertyMetadata(0));
        public static readonly DependencyProperty TotalPriceProperty =
            DependencyProperty.Register("TotalPrice", typeof(object), typeof(PurchaseItem), new PropertyMetadata(0));
        public PurchaseItem()
        {
            InitializeComponent();
        }
    }
}
