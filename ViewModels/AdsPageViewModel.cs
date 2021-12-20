using System;
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
using GoninDigital.SharedControl;

namespace GoninDigital.ViewModels
{
    class AdsPageViewModel : BaseViewModel
    {
        private Ad selectedAd;
        public Ad SelectedAd
        {
            get { return selectedAd; }
            set { selectedAd = value; OnPropertyChanged(); }
        }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
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
        private ContentDialog addAdDialog;
        public ContentDialog AddAdDialog
        {
            get { return addAdDialog; }
            set { addAdDialog = value; }
        }

        public int ProductIdToAdd { get; set; }
        public ICommand ProductDeleteCommand { get; set; }
        public ICommand ProductAddCommand { get; set; }

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

            UpdateCommand = new RelayCommand<Object>( (p) => {
                if (SelectedAd != null)
                {
                    return true;
                }
                return false;
            }, (p) =>{  UpdateExec(); });
            AddCommand = new RelayCommand<Object>((p) => true, (p) => { AddExec(); });

            ProductDeleteCommand = new RelayCommand<Product>(p => true, p => { DeleteProductExec(p); });
            ProductAddCommand = new RelayCommand<object>(p => true, p => { AddProductExec(); });

            SelectedAd = new Ad();
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
        private void UpdateExec()
        {
            
                if (SelectedAd.Subtitle == "" | SelectedAd.Title == "")
                {
                        ContentDialog content = new()
                        {
                            Title = "Warning",
                            Content = "No cell is allowed to be left blank",
                            PrimaryButtonText = "Ok"
                        };
                        content.ShowAsync();
                }
                else
                {
                    using (var db = new GoninDigitalDBContext())
                    {
                        db.Ads.Update(SelectedAd);
                        _ = db.SaveChanges();
                    }
                    ContentDialog content = new()
                    {
                        Title = "Complete",
                        Content = "Updated Successfully",
                        PrimaryButtonText = "Ok"
                    };
                    content.ShowAsync();
                }
        }
        private void AddExec()
        {
            AddAdDialog = new ContentDialog()
            {

                CloseButtonText = "Close",
                Content = new AddAdDialog(),
                Title = "Add Ad",

            };
            AddAdDialog.ShowAsync();
            AddAdDialog.CloseButtonClick += AddAdDialog_CloseButtonClick;

        }

        private void AddAdDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            using (var db = new GoninDigitalDBContext())
            { 
                L_Ads = new ObservableCollection<Ad>(db.Ads); 
            }
        }

        private void DeleteProductExec(Product product)
        {
            try
            {
                using (var db = new GoninDigitalDBContext())
                {
                    db.AdDetails.Remove(db.AdDetails.First(o => o.ProductId == product.Id && o.AdId == selectedAd.Id));
                    db.SaveChanges();
                    Load_Ads();
                }
            }
            catch (Exception ex)
            {
                var dialog = new ContentDialog
                {
                    Content = ex.Message,
                    Title = "Error",
                    CloseButtonText = "Close",
                };
                dialog.ShowAsync();
            }

        }
        private async void AddProductExec()
        {
            try
            {

                var addDialog = new ContentDialog
                {
                    Title = "Add Product",
                    Content = new ProductSearchDialog(),
                    PrimaryButtonText = "Add",
                    CloseButtonText = "Cancel",
                    PrimaryButtonCommand = new RelayCommand<object>(o => true, o => {
                        if (ProductIdToAdd != 0)
                        {
                            using (var db = new GoninDigitalDBContext())
                            {
                                var currentAdProducts = db.AdDetails.Where(o => o.AdId == SelectedAd.Id).Select(o => o.ProductId);
                                if (currentAdProducts.Contains(ProductIdToAdd))
                                {
                                    var dialog = new ContentDialog
                                    {
                                        Content = "Product existed! Nothing changed",
                                        Title = "Warning",
                                        CloseButtonText = "Close",
                                    };
                                    dialog.ShowAsync();
                                }
                                else
                                {
                                    var adDetailToAdd = new AdDetail
                                    {
                                        AdId = SelectedAd.Id,
                                        ProductId = ProductIdToAdd,
                                    };
                                    db.AdDetails.Add(adDetailToAdd);
                                    db.SaveChanges();
                                    Load_Ads();
                                }

                            }
                        }
                    }),

                };
                await addDialog.ShowAsync();
            }
            catch (Exception ex)
            {
                var dialog = new ContentDialog
                {
                    Content = ex.Message,
                    Title = "Error",
                    CloseButtonText = "Close",
                };
                await dialog.ShowAsync();
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


