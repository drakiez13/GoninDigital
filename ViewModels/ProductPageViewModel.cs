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

namespace GoninDigital.ViewModels
{
    class ProductPageViewModel : BaseViewModel
    {
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
        private int productId;
        public int ProductId
        {
            get => productId;
            set { productId = value; OnPropertyChanged(); }
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
        private long productPrice;
        public long ProductPrice
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
        public ICommand AddtoCartCommand { get; set; }
        Product product = new Product();

        public ProductPageViewModel()
        {
            product = DataProvider.Instance.Db.Products.Where(x => x.Id == 2).First();
            ratingValue = product.Rating;
            ratingCap = (product.Rating).ToString();
            productImage = product.Image;
            vendorAvatar = DataProvider.Instance.Db.Vendors.Where(x => x.Id == product.VendorId).First().Avatar;
            productName = product.Name;
            VendorName = DataProvider.Instance.Db.Vendors.Where(x=>x.Id==product.VendorId).First().Name;
            productType = DataProvider.Instance.Db.ProductCategories.Where(x=>x.Id==product.CategoryId).First().Name;
            ProductPrice = product.Price;
            if (product.DiscountRate == 0)
                IsDisc = "Hidden";
            else
                IsDisc = "Visible";
            productDescription = product.Description;
            vendorAddress= DataProvider.Instance.Db.Vendors.Where(x => x.Id == product.VendorId).First().Address;
            brandName= DataProvider.Instance.Db.Brands.Where(x=>x.Id==product.BrandId).First().Name;
            double discountPrice=Convert.ToDouble(product.Price)*(1- Convert.ToDouble(product.DiscountRate)/ 100);
            ProductDiscountPrice = discountPrice.ToString();
            var Products_of_Vendor= DataProvider.Instance.Db.Products.Where(x => x.VendorId==product.VendorId).ToList();
            vendorRating = 0;
            for(int i=0;i< Products_of_Vendor.Count();i++)
            {
                vendorRating += Products_of_Vendor[i].Rating;
            }
            vendorRating /= Products_of_Vendor.Count();
            vendorProducts = DataProvider.Instance.Db.Vendors.Where(x => x.Id == product.VendorId).Count();
            byte? @new = product.New;
            productStatus = @new.ToString()+"%";
            productAvailable = product.Available;
            AddtoCartCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { AddtoCartExecute(); });
        }
        void AddtoCartExecute()
        {
            int userID = DataProvider.Instance.Db.Users.Where(x => x.UserName == Settings.Default.usrname).First().Id;
            if (DataProvider.Instance.Db.Carts.Where(x => x.UserId==userID & x.ProductId == 2).Count() == 0)
            {
                Cart cart = new Cart();
                cart.UserId = userID;
                cart.ProductId = 2;
                cart.Quantity = 1;
                DataProvider.Instance.Db.Carts.Add(cart);
                DataProvider.Instance.Db.SaveChanges();
            }
            else
            {
                DataProvider.Instance.Db.Carts.Where(x => x.UserId == userID & x.ProductId == 2).First().Quantity += 1;
                DataProvider.Instance.Db.SaveChanges();
            }
            MessageBox.Show("This product has been added to your cart");
        }
    }
}
