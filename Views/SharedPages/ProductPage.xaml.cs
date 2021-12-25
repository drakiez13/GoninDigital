using GoninDigital.Models;
using GoninDigital.Properties;
using GoninDigital.ViewModels;
using Microsoft.EntityFrameworkCore;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Page = ModernWpf.Controls.Page;

namespace GoninDigital.Views.SharedPages
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }
            storage = value;
            OnPropertyChanged(propertyName);
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        public static Stack<Product> OldProduct { get; set; } = new Stack<Product>();
        public Product ProductInfo { get; set; }
        public ICommand BuyCommand { get; set; }
        public ICommand AddtoCartCommand { get; set; }
        public ICommand SendCommentCommand { get; set; }
        public ICommand RatingBoxCommand { get; set; }
        public string IsDisc { get; set; }

        double _userRating = 0;
        public double userRating 
        {
            get { return _userRating; }
            set { _userRating = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Comment> comments;
        public ObservableCollection<Comment> Comments
        {
            get { return comments; }
            set { comments = value; OnPropertyChanged(); }
        }
        private string newComment;
        public string NewComment
        {
            get { return newComment; }  
            set { newComment = value; OnPropertyChanged();}
        }

        public ProductPage(Product product)
        {
            using (var db = new GoninDigitalDBContext())
            {
                ProductInfo = db.Products
                    .Include(o => o.Category)
                    .Include(o => o.Brand)
                    .Include(o => o.Comments).ThenInclude(o => o.User)
                    .Include(o => o.ProductSpecDetails)
                    .ThenInclude(o => o.Spec)
                    .Include(o => o.Vendor)
                    .First(o => o.Id == product.Id);
                Comments = new ObservableCollection<Comment>(ProductInfo.Comments.ToList());
            }
            
            AddtoCartCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { AddtoCartExecute(); });
            SendCommentCommand = new RelayCommand<Window>((p) => 
            {
                if (string.IsNullOrEmpty(newComment))
                {
                    return false;
                }
                else
                {
                    return true;
                }
                
            }, (p) => { SendCommentExecute(); });
            BuyCommand = new RelayCommand<Product>(p => true, p => DashBoard.RootFrame.Navigate(new CheckoutPage(ProductInfo, 1)));
            if (product.Price == product.OriginPrice)
                IsDisc = "Hidden";
            else
                IsDisc = "Visible";

            using (GoninDigitalDBContext context = new())
            {
                User tmp = context.Users.FirstOrDefault(x => Settings.Default.usrname == x.UserName);
                var usr_rating = context.Ratings.FirstOrDefault(x => x.UserId == tmp.Id && x.ProductId == ProductInfo.Id);
                if (usr_rating != default) // user rated this product
                {
                    userRating = usr_rating.Value;
                }
                else
                {
                    userRating = 0;
                }
            }
            
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            if (e.NavigationMode != NavigationMode.Back)
                OldProduct.Push(ProductInfo);
        }

        public ProductPage() : this(OldProduct.Pop()) { }

        private void SendCommentExecute()
        {
            Comment comment = new Comment();
            comment.ProductId = ProductInfo.Id;
            comment.Time = DateTime.Now;
            comment.Value = newComment;
            using (var db = new GoninDigitalDBContext())
            {
                User user = db.Users.FirstOrDefault(x => Settings.Default.usrname == x.UserName);
                comment.UserId = user.Id;
                db.Comments.Add(comment);
                Comments.Add(comment);
                db.SaveChanges();
            }
            NewComment = null;
                
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

        private void RatingBox_ValueChanged(RatingControl sender, object args)
        {
            using (var context = new GoninDigitalDBContext())
            {
                User tmp = context.Users.FirstOrDefault(x => Settings.Default.usrname == x.UserName);
                List<Invoice> all_invoice_current_user = context.Invoices.Where(x => x.CustomerId == tmp.Id && x.StatusId == 4).ToList();
                bool isBought = false;
                foreach (var inv in all_invoice_current_user)
                {
                     isBought = (context.InvoiceDetails.Where(x => x.InvoiceId == inv.Id && x.ProductId == ProductInfo.Id).Any() && true);
                }
                
                if (isBought) // user bought product
                {
                    var usr_rating = context.Ratings.FirstOrDefault(x => x.UserId == tmp.Id && x.ProductId == ProductInfo.Id);
                    if (usr_rating == default)
                    {
                        Rating rating = new()
                        {
                            UserId = tmp.Id,
                            ProductId = ProductInfo.Id,
                            Value = (short)sender.Value,
                        };
                        context.Ratings.Add(rating);
                    }
                    else
                    {
                        usr_rating.Value = (short)sender.Value;
                    }

                    var cur_item = context.Products.FirstOrDefault(x => x.Id == ProductInfo.Id);
                    if (usr_rating == default) cur_item.NRating++;
                    cur_item.Rating = (cur_item.Rating * (cur_item.NRating - 1) + usr_rating.Value) / cur_item.NRating;
                    context.Products.Update(cur_item);
                    context.SaveChanges();
                }
                else
                {
                    ContentDialog content = new()
                    {
                        Title = "Warning",
                        Content = "You must buy item before rate for this.",
                        PrimaryButtonText = "Ok"
                    };
                    content.ShowAsync();
                }
            }
        }

        private void PersonPicture_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DashBoard.RootFrame.Navigate(new ShopPage(ProductInfo.Vendor.Id));
        }
    }
}
