using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using GoninDigital.Models;
using GoninDigital.Properties;
using Microsoft.EntityFrameworkCore;
using GoninDigital.SharedControl;
using ModernWpf.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using GoninDigital.Utils;

namespace GoninDigital.ViewModels
{
    class MyShopViewModel : BaseViewModel
    {
        private ContentDialog upgradeDialog;
        public ContentDialog UpgradeDialog
        {
            get { return upgradeDialog; }
            set { upgradeDialog = value; }
        }
        private bool hasVendor;
        public bool HasVendor
        {

            get { return hasVendor; }
            set { hasVendor = value; OnPropertyChanged(); }
        }

        private bool isUpgrade;
        public bool IsUpgrade
        {

            get { return isUpgrade; }
            set { isUpgrade = value; OnPropertyChanged(); }
        }
        private bool isNameAvailable = true;
        public bool IsNameAvailable
        {

            get { return isNameAvailable; }
            set { isNameAvailable = value; OnPropertyChanged(); }
        }
        private Product selectedItem = null;
        public Product SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(); }
        }
        private Vendor vendor = null;

        public Vendor Vendor
        {
            get { return vendor; }
            set { vendor = value; OnPropertyChanged(); }
        }
        private List<ProductSpecDetail> selectedProductSpecs;
        public List<ProductSpecDetail> SelectedProductSpecs
        {
            get { return selectedProductSpecs; }
            set { selectedProductSpecs = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Product> products = null;
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set { products = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Product> productBestSeller = null;
        public ObservableCollection<Product> ProductBestSeller
        {
            get { return productBestSeller; }
            set { productBestSeller = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Product> productSpecial = null;
        public ObservableCollection<Product> ProductSpecial
        {
            get { return productSpecial; }
            set { productSpecial = value; OnPropertyChanged(); }
        }
        private Vendor newVendor;
        public Vendor NewVendor
        {
            get { return newVendor; }
            set { newVendor = value; OnPropertyChanged(); }
        }
        private string vendorName = null;
        public string VendorName
        {
            get { return vendorName; }
            set { vendorName = value; OnPropertyChanged(); }
        }
        private List<string> allVendorNames = null;
        public List<string> AllVendorNames
        {
            get { return allVendorNames; }
            set { allVendorNames = value; OnPropertyChanged(); }
        }
        private Product productClone;
        public Product ProductClone
        {
            get { return productClone; }
            set { productClone = value; OnPropertyChanged(); }
        }
        private List<string> categoryList;
        public List<string> CategoryList
        {
            get { return categoryList; }
            set { categoryList = value; OnPropertyChanged(); }
        }
        private List<string> brandList;
        public List<string> BrandList
        {
            get { return brandList; }
            set { brandList = value; OnPropertyChanged(); }
        }

        public int OnPrimaryButtonClick { get; private set; }
        public int PrimaryButtonClick { get; private set; }


        public ICommand EditAvatarCommand { get; set; }
        public ICommand EditCoverPhotoCommand { get; set; }

        public ICommand AddCommand { get; set; }
        public void AddCommandExec(object o)
        {
            
            using (var db = new GoninDigitalDBContext())
            {
                selectedItem = new Product
                {
                    Name = "Name",
                    VendorId = Vendor.Id,
                    CategoryId = db.ProductCategories.First().Id,
                    Description = "Unknown",
                    Origin = "Viet Nam",
                    Price = 1000,
                    OriginPrice = 1000,
                    StatusId =(int)Constants.ProductStatus.CREATED,
                    CreatedAt=DateTime.Now,
                    UpdatedAt=DateTime.Now,
                    BrandId=db.Brands.First().Id,
                    NRating=0,
                    Available=0,
                    Buy=0,

                };
                
                db.Products.Add(selectedItem);
                Products.Add(selectedItem);
                db.SaveChanges();
            }
            UpdateCategorySpecDetails();
            var dialog = new ContentDialog
            {
                Content = new AddProductDialog(),

                Title = "Add Product",
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",

                PrimaryButtonCommand = new RelayCommand<object>((p) => true, (p) => { AddBtnExec(); }),
                CloseButtonCommand = new RelayCommand<object>((p) => true, (p) => { CloseAddBtnExec(); }),
            };
            dialog.ShowAsync();
        }

        public ICommand EditCommand { get; set; }
        public void EditCommandExec(object o)
        {
            UpdateCategorySpecDetails();

            var dialog = new ContentDialog
            {
                Content = new EditProductDialog(),

                Title = "Edit Product",
                PrimaryButtonText = "Change",
                CloseButtonText = "Cancel",

                PrimaryButtonCommand = new RelayCommand<object>((p) => true, (p) => { EditBtnExec(); }),
                CloseButtonCommand = new RelayCommand<object>((p) => true, (p) => { CloseBtnExec(); }),
            };
            dialog.ShowAsync();
        }
        public ICommand UpgradeVendorCommand { get; set; }
        public ICommand CloseUpgradeBDCommand { get; set; }
        public void CloseUpgradeBDExec()
        {
            upgradeDialog.Hide();
        }
        public ICommand UpgradeCommand { get; set; }
        public void UpgradeCommandExec()
        {
            if (upgradeDialog == null)
            {
                upgradeDialog = new ContentDialog
                {
                    Content = new UpgradeVendorDialog(),

                    Title = "Upgrade",
                };
            }
            upgradeDialog.ShowAsync();
        }
        public ICommand RemoveCommand { get; set; }
        public void RemoveCommandExec(object o)
        {
            using (var db = new GoninDigitalDBContext())
            {
                try
                {
                    SelectedItem.StatusId = (int)Constants.ProductStatus.REMOVED;
                    Products.Remove(SelectedItem);
                    db.Update(SelectedItem);
                    _ = db.SaveChanges();
                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message);
                }
            }
        }
        public ICommand SaveVendorInfoCommand { get; set; }
        public void SaveVendorConfirm()
        {
            var dialog = new ContentDialog
            {
                Content = "Do you want to change your vendor information ?",

                Title = "Confirm",
                PrimaryButtonText = "Save",
                CloseButtonText = "Cancel",
                CloseButtonCommand = new RelayCommand<object>((p) => true, (p) => { ResetVendorInfoExec(); }),
                PrimaryButtonCommand = new RelayCommand<object>((p) => true, (p) => { SaveVendorInfoExec(); }),
            };
            dialog.ShowAsync();
        }
        public ICommand ResetVendorInfoCommand { get; set; }
        public void ResetVendorInfoExec()
        {
            using (var db = new GoninDigitalDBContext())
            {
                Vendor = db.Vendors.Include(o => o.Owner)
                    .Include(o => o.Products)
                    .First(o => o.Owner.UserName == Settings.Default.usrname);
                db.ProductCategories.ToList();
                VendorName = Vendor.Name;
            }
        }
        public async void SaveVendorInfoExec()
        {
            using (var db = new GoninDigitalDBContext())
            {
                try
                {
                    Vendor.Name = VendorName;
                    db.Vendors.Update(Vendor);
                    await db.SaveChangesAsync();
                    var dialog = new ContentDialog
                    {
                        Title = "Completed",
                        Content = "Your vendor infomation is saved",
                        PrimaryButtonText = "Ok"
                    };
                    await dialog.ShowAsync();
                }
                catch (Exception e)
                {

                    var dialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = e.Message,
                        PrimaryButtonText = "Ok"
                    };
                    await dialog.ShowAsync();
                }
            }
        }
        public ICommand ImageEditCommand { get; set; }
        public async void ImageEditCommandExec(object o)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Choose Image..";

            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                var linkAvatar = await ImageUploader.UploadAsync(openFileDialog.FileName);
                using (var db = new GoninDigitalDBContext())
                {
                    SelectedItem.Image = linkAvatar;

                    db.Update(SelectedItem);
                    _ = db.SaveChanges();
                }
            }
        }

        public async void EditAvatarExec()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Choose Image..";

            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                var linkAvatar = await ImageUploader.UploadAsync(openFileDialog.FileName);
                using (var db = new GoninDigitalDBContext())
                {
                    Vendor.Avatar = linkAvatar;
                    db.Vendors.Update(Vendor);
                    _ = db.SaveChanges();
                    Vendor = db.Vendors.Include(o => o.Owner)
                            .Include(o => o.Products)
                            .First(o => o.Owner.UserName == Settings.Default.usrname);
                }
            }

        }
        public async void EditCoverPhotoExec()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Choose Image..";

            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                var linkAvatar = await ImageUploader.UploadAsync(openFileDialog.FileName);
                using (var db = new GoninDigitalDBContext())
                {
                    Vendor.Cover = linkAvatar;
                    db.Vendors.Update(Vendor);
                    _ = db.SaveChanges();
                    Vendor = db.Vendors.Include(o => o.Owner)
                            .Include(o => o.Products)
                            .First(o => o.Owner.UserName == Settings.Default.usrname);
                }
            }

        }

        public void CloseBtnExec()
        {
           
            using (var db = new GoninDigitalDBContext())
            {
                db.Entry(selectedItem).Reload();
                Products = new ObservableCollection<Product>(Vendor.Products.Where(o => o.StatusId == (int)Constants.ProductStatus.ACCEPTED).ToList());
            }

        }
        public void EditBtnExec()
        {

            using (var db = new GoninDigitalDBContext())
            {

                SelectedProductSpecs.RemoveAll(o => string.IsNullOrEmpty(o.Value));
                SelectedProductSpecs.ForEach(o =>
                {
                    o.Spec = null;
                });
                db.ProductSpecDetails.AddRange(selectedProductSpecs);
                db.Entry(SelectedItem).State = EntityState.Modified;
                db.SaveChanges();
            }

        }
        public void UpgradeExec()
        {

            using (var db = new GoninDigitalDBContext())
            {
                int userId = db.Users.First(u => u.UserName == Settings.Default.usrname).Id;
                newVendor.OwnerId = userId;
                newVendor.ApprovalStatus = (int)Constants.ApprovalStatus.WAITING;
                db.Vendors.Add(newVendor);
                Vendor = newVendor;
                IsUpgrade = true;
                db.SaveChanges();
            }
            upgradeDialog.Hide();
        }
        public void UpdateCategorySpecDetails()
        {
            using (var db = new GoninDigitalDBContext())
            {
                selectedProductSpecs = db.ProductSpecDetails.Where(o => o.ProductId == selectedItem.Id && o.Spec.CategoryId == selectedItem.CategoryId).ToList();
                var temp = selectedProductSpecs.Select(o => o.SpecId).ToList();
                var availSpecType = db.ProductSpecs.Where(o => o.CategoryId == selectedItem.CategoryId).ToList();
                availSpecType.ForEach(specType =>
                {
                    if (!temp.Contains(specType.Id))
                    {
                        selectedProductSpecs.Add(new ProductSpecDetail
                        {
                            ProductId = selectedItem.Id,
                            SpecId = specType.Id,
                            Spec = specType,
                        });

                    }
                });
                SelectedProductSpecs = selectedProductSpecs;
            }
        }
        public void AddBtnExec()
        {
            using (var db = new GoninDigitalDBContext())
            {
                if (selectedItem != null)
                {
                    try
                    {
                        
                        db.ProductSpecDetails.AddRange(SelectedProductSpecs);
                        db.Update(selectedItem);
                        db.SaveChanges();
                    }
                    catch
                    {
                        var content = new ContentDialog();
                        content.Content = "An unexpected error occured!";
                        content.Title = "Warning";
                        content.PrimaryButtonText = "Ok";
                    }
                }
            }
        }
        public void CloseAddBtnExec()
        {
            using (var db = new GoninDigitalDBContext())
            {
                if (selectedItem != null)
                {
                    db.Products.Remove(selectedItem);
                    Products.Remove(selectedItem);
                    db.SaveChanges();
                }
            }
        }

        public MyShopViewModel()
        {

            using (var db = new GoninDigitalDBContext())
            {
                categoryList = db.ProductCategories.Select(o => o.Name).ToList();
                brandList = db.Brands.Select(o => o.Name).ToList();
                if (db.Users.First(o => o.UserName == Settings.Default.usrname).TypeId == (int)Constants.UserType.CUSTOMER)
                {
                    HasVendor = false;
                    try
                    {
                        Vendor = db.Vendors.Include(o => o.Owner)
                            .Include(o => o.Products)
                            .First(o => o.Owner.UserName == Settings.Default.usrname);
                        IsUpgrade = true;
                    }
                    catch
                    {
                        var query = from o in db.Vendors select o.Name;
                        AllVendorNames = query.ToList();
                        IsUpgrade = false;
                    }
                }
                else
                {
                    Vendor = db.Vendors.Include(o => o.Owner)
                        .Include(o => o.Products)
                        .ThenInclude(o => o.Brand)
                        .Include(o => o.Products)
                        .ThenInclude(o => o.Category)
                        .ThenInclude(o => o.ProductSpecs)
                        .ThenInclude(o => o.ProductSpecDetails)

                        .First(o => o.Owner.UserName == Settings.Default.usrname);

                    Products = new ObservableCollection<Product>(Vendor.Products.Where(o => o.StatusId == (int)Constants.ProductStatus.ACCEPTED).ToList());

                    if (Products.Count() >= 10)
                    {
                        ProductBestSeller = new ObservableCollection<Product>(Vendor.Products.Where(o => o.StatusId == (int)Constants.ProductStatus.ACCEPTED).OrderByDescending(o => o.Buy).Take(10).ToList());
                        ProductSpecial = new ObservableCollection<Product>(Vendor.Products.Where(o => o.StatusId == (int)Constants.ProductStatus.ACCEPTED).OrderByDescending(o => o.Rating).Take(10).ToList());
                    }
                    else
                    {
                        ProductBestSeller = new ObservableCollection<Product>(Vendor.Products.Where(o => o.StatusId == (int)Constants.ProductStatus.ACCEPTED).OrderByDescending(o => o.Buy).Take(Products.Count()).ToList());
                        ProductSpecial = new ObservableCollection<Product>(Vendor.Products.Where(o => o.StatusId == (int)Constants.ProductStatus.ACCEPTED).OrderByDescending(o => o.Rating).Take(10).ToList());
                    }
                    
                    HasVendor = true;
                    VendorName = Vendor.Name;
                }
            }
            productClone = new Product();
            newVendor = new Vendor();
            AddCommand = new RelayCommand<Product>(o => true, o => AddCommandExec(o));
            EditCommand = new RelayCommand<Product>(o => true, o => EditCommandExec(o));
            RemoveCommand = new RelayCommand<Product>(o => true, o => RemoveCommandExec(o));
            ImageEditCommand = new RelayCommand<Product>(o => true, o => ImageEditCommandExec(o));
            EditCoverPhotoCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { EditCoverPhotoExec(); });
            EditAvatarCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { EditAvatarExec(); });
            ResetVendorInfoCommand = new RelayCommand<object>((p) => true, (p) => { ResetVendorInfoExec(); });

            UpgradeCommand = new RelayCommand<object>((p) => true, (p) => { UpgradeCommandExec(); });
            UpgradeVendorCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(newVendor.Name))
                {
                    return false;
                }
                if (allVendorNames.Any(s => newVendor.Name.Contains(s)))
                {
                    isNameAvailable = false;
                    return false;
                }

                return true;
            }, (p) => { UpgradeExec(); });
            CloseUpgradeBDCommand = new RelayCommand<object>((p) => true, (p) => { CloseUpgradeBDExec(); });

            SaveVendorInfoCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(VendorName))
                {
                    return false;
                }

                return true;
            }, (p) => { SaveVendorConfirm(); });
        }

    }
}
