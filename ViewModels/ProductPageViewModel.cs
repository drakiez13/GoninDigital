using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GoninDigital.Models;
namespace GoninDigital.ViewModels
{
    class ProductPageViewModel : BaseViewModel
    {
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
        private string vendorRating;
        public string VendorRating
        {
            get => vendorRating;
            set { vendorRating = value; OnPropertyChanged(); }
        }
        private string vendorProducts;
        public string VendorProducts
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
        private string productAvailable;
        public string ProductAvailable
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

        public ProductPageViewModel()
        {
            ratingValue = 5;
            ratingCap = "100";
            productImage = "/Resources/Images/BlankImage.jpg";
            vendorAvatar = "/Resources/Images/LoginImage.jpg";
            productName = "Màn hình HP 24'' 1JS08A4 (1920 x 1200/IPS/60Hz/5 ms)";
            vendorName = "Ngoc Huy Store";
            productType = "Vang 999";
            productPrice = "1300000";
            productDescription = "Ram tự động ép xung giúp nó lên tần số cao nhất khi được công bố, lên đến 2666MHz, nhằm cung cấp hiệu năng cao nhất cho các bo mạch chủ có chipset 100 Series và x99 của Intel. Tăng hiệu suất tối đa cho các vi xử lý 2, 4, 6, 8-core của Intel giúp các tác vụ chỉnh sửa video, dựng phim 3D, chơi game... xử lý nhanh chóng. Thiết kế bộ tản nhiệt độc đáo có độ cao thấp mạnh mẽ giúp tương thích với nhiều loại case có kích thước nhỏ cho hệ thống của bạn thêm chuyên nghiệp.";
            vendorAddress= "Ho Chi Minh";
            brandName= "MSI";
            productDiscountPrice = "1200000 VND";
            vendorRating = "4.3";
            vendorProducts = "2000";
            productStatus = "100%";
            productAvailable = "123";
        }
    }
}
