using GoninDigital.Models;
using GoninDigital.Properties;
using GoninDigital.ViewModels;
using ModernWpf.Controls;
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
using Page = ModernWpf.Controls.Page;

namespace GoninDigital.Views.SharedPages
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public Product ProductInfo { get; set; }
        public ICommand BuyCommand { get; set; }
        public ICommand AddtoCartCommand { get; set; }
        public string IsDisc { get; set; }
        

        public ProductPage(Product product)
        {
            ProductInfo = product;
            AddtoCartCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { AddtoCartExecute(); });
            BuyCommand = new RelayCommand<Product>(p => true, p => DashBoard.RootFrame.Navigate(new CheckoutPage(ProductInfo, 1)));
            if (product.Price == product.OriginPrice)
                IsDisc = "Hidden";
            else
                IsDisc = "Visible";
            InitializeComponent();
        }

        void AddtoCartExecute()
        {
            using (var context = new GoninDigitalDBContext())
            {
                int userID = context.Users.Where(x => x.UserName == Settings.Default.usrname).First().Id;
                if (context.Carts.Where(x => x.UserId == userID & x.ProductId == ProductInfo.Id).Count() == 0)
                {
                    Cart cart = new Cart();
                    cart.UserId = userID;
                    cart.ProductId = ProductInfo.Id;
                    cart.Quantity = 1;
                    context.Carts.Add(cart);
                    context.SaveChanges();
                }
                else
                {
                    context.Carts.Where(x => x.UserId == userID && x.ProductId == ProductInfo.Id).First().Quantity += 1;
                    context.SaveChanges();
                }
                ContentDialog content = new()
                {
                    Title = "Completed",

                    Content = "This product has been added to your cart",
                    PrimaryButtonText = "Ok"
                };
                content.ShowAsync();
            }
            
        }
    }
}
