using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GoninDigital.Models;
using System.Windows.Input;
using GoninDigital.Views.SharedPages;
using GoninDigital.Properties;
using ModernWpf.Controls;

namespace GoninDigital.ViewModels
{
    class ProductPageViewModel : BaseViewModel
    {
        #region Properties
        private int productId;
        public int ProductId
        {
            get => productId;
            set { productId = value; OnPropertyChanged(); }
        }
        private string isDisc;
        public string IsDisc
        {
            get { return isDisc; }
            set { isDisc = value; OnPropertyChanged(); }
        }
        private string productImage;
        public string ProductImage
        {
            get { return productImage; }
            set { productImage = value; OnPropertyChanged(); }
        }
        private string vendorAvatar;
        public string VendorAvatar
        {
            get { return vendorAvatar; }
            set { vendorAvatar = value; OnPropertyChanged(); }
        }
        private string productType;
        public string ProductType
        {
            get => productType;
            set { productType = value; OnPropertyChanged(); }
        }
        private string productName;
        public string ProductName
        {
            get => productName;
            set { productName = value; OnPropertyChanged(); }
        }
        private int ratingValue;
        public int RatingValue
        {
            get => ratingValue;
            set { ratingValue = value; OnPropertyChanged(); }
        }
        private string ratingCap;
        public string RatingCap
        {
            get => ratingCap;
            set { ratingCap = value; OnPropertyChanged(); }
        }
        private string productDescription;
        public string ProductDescription
        {
            get => productDescription;
            set { productDescription = value; OnPropertyChanged(); }
        }
        private string vendorName;
        public string VendorName
        {
            get => vendorName;
            set { vendorName = value; OnPropertyChanged(); }
        }
        private string productPrice;
        public string ProductPrice
        {
            get => productPrice;
            set { productPrice = value; OnPropertyChanged(); }
        }
        private string vendorAddress;
        public string VendorAddress
        {
            get => vendorAddress;
            set { vendorAddress = value; OnPropertyChanged(); }
        }
        private float vendorRating;
        public float VendorRating
        {
            get => vendorRating;
            set { vendorRating = value; OnPropertyChanged(); }
        }
        private int vendorProducts;
        public int VendorProducts
        {
            get => vendorProducts;
            set { vendorProducts = value; OnPropertyChanged(); }
        }
        private string brandName;
        public string BrandName
        {
            get => brandName;
            set { brandName = value; OnPropertyChanged(); }
        }
        private string productStatus;
        public string ProductStatus
        {
            get => productStatus;
            set { productStatus = value; OnPropertyChanged(); }
        }
        private int productAvailable;
        public int ProductAvailable
        {
            get => productAvailable;
            set { productAvailable = value; OnPropertyChanged(); }
        }
        private string productDiscount;
        public string ProductDiscount
        {
            get => productDiscount;
            set { productDiscount = value; OnPropertyChanged(); }
        }
        private string productDiscountPrice;
        public string ProductDiscountPrice
        {
            get => productDiscountPrice;
            set { productDiscountPrice = value; OnPropertyChanged(); }
        }
        Product product = new Product();
        GoninDigitalDBContext context = new();
        public ICommand AddtoCartCommand { get; set; }
        #endregion
        #region Constructor
        public ProductPageViewModel(int id)
        {
            ProductId = id;
            product = context.Products.Where(x => x.Id == ProductId).First(); //ID mặc định
            loadInfo();
            AddtoCartCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { AddtoCartExecute(); });
        }

        public ProductPageViewModel()
        {
            product = context.Products.Where(x => x.Id == 2).First();
            loadInfo();
            AddtoCartCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { AddtoCartExecute(); });
        }
        #endregion
        #region Private Methods
        void loadInfo()
        {
            ratingValue = product.Rating;
            ratingCap = (product.Rating).ToString();
            productImage = product.Image;
            vendorAvatar = context.Vendors.Where(x => x.Id == product.VendorId).First().Avatar;
            productName = product.Name;
            VendorName = context.Vendors.Where(x => x.Id == product.VendorId).First().Name;
            productType = context.ProductCategories.Where(x => x.Id == product.CategoryId).First().Name;
            ProductPrice = String.Format("{0:0,0 vnđ}", product.OriginPrice);
            if (product.Price == product.OriginPrice)
                IsDisc = "Hidden";
            else
                IsDisc = "Visible";
            productDescription = product.Description;
            vendorAddress = context.Vendors.Where(x => x.Id == product.VendorId).First().Address;
            brandName = context.Brands.Where(x => x.Id == product.BrandId).First().Name;
            ProductDiscountPrice = string.Format("{0:0,0 vnđ}", product.Price);
            var Products_of_Vendor = context.Products.Where(x => x.VendorId == product.VendorId).ToList();
            vendorRating = 0;
            for (int i = 0; i < Products_of_Vendor.Count(); i++)
            {
                vendorRating += Products_of_Vendor[i].Rating;
            }
            vendorRating /= Products_of_Vendor.Count();
            vendorProducts = context.Products.Where(x => x.VendorId == product.VendorId).Count();
            productStatus = ((byte?)product.New).ToString() + "%";
            productAvailable = product.Available;
        }
        void AddtoCartExecute()
        {
            int userID = context.Users.Where(x => x.UserName == Settings.Default.usrname).First().Id;
            if (context.Carts.Where(x => x.UserId == userID & x.ProductId == ProductId).Count() == 0) //Id mặc định
            {
                Cart cart = new Cart();
                cart.UserId = userID;
                cart.ProductId = ProductId; //Id mặc định
                cart.Quantity = 1;
                context.Carts.Add(cart);
                context.SaveChanges();
            }
            else
            {
                context.Carts.Where(x => x.UserId == userID & x.ProductId == ProductId).First().Quantity += 1; //Id mặc định
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
        #endregion
    }
}