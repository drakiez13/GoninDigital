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
        public object Id
        {
            get => (object)GetValue(IdProperty);
            set => SetValue(IdProperty, value);
        }
        public object Title
        {
            get => (object)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public object Image
        {
            get => (object)GetValue(ImageProperty);
            set => SetValue(RatingValueProperty, value);
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

        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(object), typeof(ProductCard), new PropertyMetadata(null));
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(object), typeof(ProductCard), new PropertyMetadata("Unknown"));
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(object), typeof(ProductCard),
                new PropertyMetadata("/Resources/Images/BlankImage.jpg"), new ValidateValueCallback((object value) => { return value != null; }));
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

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            if (Id != null)
            {
                MessageBox.Show("Navigate to product page");
            }
        }
    }
}
