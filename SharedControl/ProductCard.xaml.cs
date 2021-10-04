using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for ProductCard.xaml
    /// </summary>
    public partial class ProductCard : UserControl
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
        public object RatingValue
        {
            get => (object)GetValue(RatingValueProperty);
            set => SetValue(RatingValueProperty, value);
        }
        public object RatingCaption
        {
            get => (object)GetValue(RatingCaptionProperty);
            set => SetValue(RatingCaptionProperty, value);
        }
        public object Price
        {
            get => (object)GetValue(PriceProperty);
            set => SetValue(PriceProperty, value);
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(object), typeof(ProductCard), new PropertyMetadata("Unknown"));
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(object), typeof(ProductCard),
                new PropertyMetadata("/Resources/BlankImage.jpg") );
        public static readonly DependencyProperty RatingValueProperty =
            DependencyProperty.Register("RatingValue", typeof(object), typeof(ProductCard), new PropertyMetadata("2.5"));
        public static readonly DependencyProperty RatingCaptionProperty =
            DependencyProperty.Register("RatingCaption", typeof(object), typeof(ProductCard), new PropertyMetadata("Rating"));
        public static readonly DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(object), typeof(ProductCard), new PropertyMetadata(0));


        public ProductCard()
        {
            InitializeComponent();
        }
    }
}
