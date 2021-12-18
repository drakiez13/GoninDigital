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
    /// Interaction logic for VendorAvatar.xaml
    /// </summary>
    public partial class VendorAvatar : UserControl
    {
        public Vendor VendorInfo
        {
            get => (Vendor)GetValue(VendorProperty);
            set => SetValue(VendorProperty, value);
        }
        public bool Clickable
        {
            get => (bool)GetValue(ClickableProperty);
            set => SetValue(ClickableProperty, value);
        }

        public static readonly DependencyProperty VendorProperty =
            DependencyProperty.Register("VendorInfo", typeof(Vendor), typeof(VendorAvatar), new PropertyMetadata(new Vendor()));
        public static readonly DependencyProperty ClickableProperty =
            DependencyProperty.Register("Clickable", typeof(bool), typeof(VendorAvatar), new PropertyMetadata(true));

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            if (VendorInfo != null && Clickable)
            {
                DashBoard.RootFrame.Navigate(new ShopPage(VendorInfo.Id));
            }
        }
        public VendorAvatar()
        {
            InitializeComponent();
        }
    }
}
