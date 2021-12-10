using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GoninDigital.Models;
using Microsoft.EntityFrameworkCore;

namespace GoninDigital.ViewModels
{
    class HomePageViewModel : BaseViewModel
    {
        private string art;
        public string Art
        {
            get { return art; }
            set { art = value; OnPropertyChanged(); }
        }

        List<Ad> ads = null;
        public List<Ad> Ads
        {
            get { return ads; }
            set { ads = value; OnPropertyChanged(); }
        }

        private List<List<Product>> adProducts = null;
        public List<List<Product>> AdProducts
        {
            get { return adProducts; }
            set { adProducts = value; OnPropertyChanged(); }
        }

        private List<Product> topProducts = null;
        public List<Product> TopProducts
        {
            get { return topProducts; }
            set { topProducts = value; OnPropertyChanged(); }
        }

        private List<Product> recommendedProducts = null;
        public List<Product> RecommendedProducts
        {
            get { return recommendedProducts; }
            set { recommendedProducts = value; OnPropertyChanged(); }
        }

        private List<Product> discountProducts = null;
        public List<Product> DiscountProducts
        {
            get { return discountProducts; }
            set { discountProducts = value; OnPropertyChanged(); }
        }

        private async void InitAds()
        {
            using (var db = new GoninDigitalDBContext())
            {
                Ads = db.Ads.OrderBy(o => Guid.NewGuid()).Take(3).ToList();
                List<List<Product>> _adProducts = new List<List<Product>>(3);
                for (int i = 0; i < 3; i++)
                {
                    _adProducts.Add(await db.AdDetails.Where(o => o.AdId == Ads[i].Id)
                                                .Include(x => x.Product.Vendor)
                                                .Include(x => x.Product.Brand)
                                                .Select(o => o.Product)
                                                .ToListAsync());

                }
                AdProducts = _adProducts;
            }
        }

        private async void InitProducts()
        {
            using (var db = new GoninDigitalDBContext())
            {
                // Selection algorithm goes here
                TopProducts = await db.Products
                    .Include(x => x.Vendor)
                    .Include(x => x.Brand)
                    .OrderBy(o => Guid.NewGuid()).Take(6).ToListAsync();

                RecommendedProducts = await db.Products
                    .Include(x => x.Vendor)
                    .Include(x => x.Brand)
                    .OrderBy(o => Guid.NewGuid()).Take(6).ToListAsync();

                DiscountProducts = await db.Products
                    .Include(x => x.Vendor)
                    .Include(x => x.Brand)
                    .OrderBy(o => Guid.NewGuid()).Take(6).ToListAsync();
            }
        }

        public HomePageViewModel()
        {
            art = "/Resources/Images/HomeBanner.jpg";

            ads = new List<Ad>(3);
            adProducts = new List<List<Product>>(3);

            InitAds();
            InitProducts();
        }
    }
}