using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using GoninDigital.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows.Input;
using ModernWpf.Controls;

namespace GoninDigital.ViewModels
{
    class AdsPageViewModel : BaseViewModel
    {
        private Ad selectedAd;
        public Ad SelectedAd
        { get { return selectedAd; } set { selectedAd = value; OnPropertyChanged(); } }
        public ICommand DeleteCommand { get; set; }
        private ObservableCollection<Product> adProducts = null;
        public ObservableCollection<Product> AdProducts
        {
            get { return adProducts; }
            set { adProducts = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Ad> l_Ads;
        public ObservableCollection<Ad> L_Ads
        {
            get { return l_Ads; }
            set { l_Ads = value; OnPropertyChanged(); }
        }
        private string searchName;
        public string SearchName
        {
            get { return searchName; }
            set { searchName = value; OnPropertyChanged(); }
        }
        public ICommand ProductDeleteCommand { get; set; }
        public AdsPageViewModel()
        {
            using (var db = new GoninDigitalDBContext())
            {
                L_Ads = new ObservableCollection<Ad>(db.Ads);
            }
            DeleteCommand = new RelayCommand<Object>((p) =>
            {
                if (SelectedAd != null)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                DeleteExec();
            });
            ProductDeleteCommand = new RelayCommand<Product>(p=> true, p => { DeleteProductExec(p); });
            SelectedAd = new Ad();
            AdProducts = new ObservableCollection<Product>();
        }
        private void DeleteExec()
        {
            using (var db = new GoninDigitalDBContext())
            {
                var ad = db.Ads.First(x => x.Id == SelectedAd.Id);
                int count = 0;
                while (count < L_Ads.Count())
                {
                    if (ad.Id == L_Ads[count].Id)
                    {

                        L_Ads.RemoveAt(count);

                        break;
                    }
                    count += 1;
                }
                var addetails = db.AdDetails.Where(x => x.AdId == ad.Id);
                db.AdDetails.RemoveRange(addetails);
                db.Ads.Remove(ad);
                db.SaveChanges();
            }
            SelectedAd = new Ad();
            AdProducts = new ObservableCollection<Product>();
        }
        private void DeleteProductExec(Product product)
        {
            try
            {
                using (var db = new GoninDigitalDBContext())
                {
                    db.AdDetails.Remove(db.AdDetails.First(o => o.ProductId == product.Id && o.AdId == selectedAd.Id));
                    AdProducts.Remove(product);
                    AdProducts = AdProducts;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
            
        }
        public async void Load_Ads()
        {
            using (var db = new GoninDigitalDBContext())
            {
                if (SelectedAd != null)
                    AdProducts = new ObservableCollection<Product>(await db.AdDetails.Where(o => o.AdId == SelectedAd.Id)
                                                .Include(x => x.Product.Vendor)
                                                .Include(x => x.Product.Brand)
                                                .Select(o => o.Product)
                                                .ToListAsync());
            }
        }
        public void SearchChanged()
        {
            if (SearchName == "")
            {
                using (var db = new GoninDigitalDBContext())
                {
                    L_Ads = new ObservableCollection<Ad>(db.Ads);
                }
            }
        }
        public void SearchAd()
        {
            string s = SearchName.ToLower();
            if (SearchName != "")
            {
                using (var db = new GoninDigitalDBContext())
                {
                    L_Ads = new ObservableCollection<Ad>(db.Ads);
                }
                int count = 0;
                while (count < L_Ads.Count())
                {
                    if (!L_Ads[count].Title.ToLower().Contains(s))
                        L_Ads.RemoveAt(count);
                    else
                        count += 1;
                }
            }
        }


    }
}


