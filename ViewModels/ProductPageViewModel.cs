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
        private string image;
        public string Image
        {
            get { return image; }
            set { image = value; OnPropertyChanged(); }
        }
        private int productId;
        public int ProductId
        {
            get => productId;
            set { productId = value; OnPropertyChanged(); }
        }
        private string typeP;
        public string TypeP
        {
            get => typeP;
            set { typeP = value; OnPropertyChanged(); }
        }
        private string nameP;
        public string NameP
        {
            get => nameP;
            set { nameP = value; OnPropertyChanged(); }
        }
        private int ratingValue;
        public int RatingValue
        {
            get => ratingValue;
            set { ratingValue = value; OnPropertyChanged(); }
        }
        private int ratingCap;
        public int RatingCap
        {
            get => ratingCap;
            set { ratingCap = value; OnPropertyChanged(); }
        }
        private string descP;
        public string DescP
        {
            get => descP;
            set { descP = value; OnPropertyChanged(); }
        }
        private string nameV;
        public string NameV
        {
            get => nameV;
            set { nameV = value; OnPropertyChanged(); }
        }
        private string priceP;
        public string PriceP
        {
            get => priceP;
            set { priceP = value; OnPropertyChanged(); }
        }
        private string addrV;
        public string AddrV
        {
            get => addrV;
            set { addrV = value; OnPropertyChanged(); }
        }
        private string nameB;
        public string NameB
        {
            get => nameB;
            set { nameB = value; OnPropertyChanged(); }
        }
        private string discP;
        public string DiscP
        {
            get => discP;
            set { discP = value; OnPropertyChanged(); }
        }
        private string priceP_now;
        public string PriceP_now
        {
            get => priceP_now;
            set { priceP_now = value; OnPropertyChanged(); }
        }
        private string priceP_origin;
        public string PriceP_origin
        {
            get => priceP_origin;
            set { priceP_origin = value; OnPropertyChanged(); }
        }

        Product product = new Product();
        public ProductPageViewModel()
        {
            ratingValue = 5;
            ratingCap = 100;
            image = "/Resources/Images/BlankImage.jpg";
            product = DataProvider.Instance.Db.Products.Where(x => x.Id == 3).First();
            NameP = product.Name;
            NameV = DataProvider.Instance.Db.Vendors.Where(x => x.Id == product.VendorId).First().Name.ToString();
            TypeP= DataProvider.Instance.Db.ProductCategories.Where(x => x.Id == product.CategoryId).First().Name;
            PriceP = product.Price.ToString();
            DescP = product.Description;
            AddrV= DataProvider.Instance.Db.Vendors.Where(x => x.Id == product.VendorId).First().Address.ToString();
            NameB= DataProvider.Instance.Db.Brands.Where(x => x.Id == product.BrandId).First().Name.ToString();
            DiscP = product.DiscountRate.ToString()+"%";
            float price = float.Parse(PriceP) * (1- float.Parse(product.DiscountRate.ToString()) /100);
            PriceP_now = price.ToString()+" VND";
            if (discP != "0")
                PriceP_origin = priceP;
            else
                PriceP_origin = "";
        }
    }
}
