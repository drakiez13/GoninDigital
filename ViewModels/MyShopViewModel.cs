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
        private ObservableCollection<Product> productCreated = null;
        public ObservableCollection<Product> ProductCreated
        {
            get { return productCreated; }
            set { productCreated = value; OnPropertyChanged(); }
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
                    Detail="Unknown",
                    Image= "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOMAAADeCAMAAAD4tEcNAAAAsVBMVEXu7u4REiRmZmYAAADX19jz8/NiYmJ9fX2MjIzl5eXt7e1WVlb29vZ0dHRoaGjb29sAABsAABhbW1sLDCB5eXkAAAkAABRtbXbOztEGCB6UlZy9vb3Dw8OXl5eBgYFxcXEAABBbW2WdnaVGRlM+PkqFhYwZGiosLTs0NEAkJTKEhIq0tLSgoKB2d38MDyW4t7xmZ3FPUFqrqq5TVFsgITEWFyk+QEgwMTx6eoVhYW06O0lY5vjcAAAEdklEQVR4nO3aC1eqShyHYZBLwskdOCkwmQk4KuxIxjC3fv8PdgZvibc6q9oK5/eUXcBavv1hpFaSBAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPBjzDMkUyp89v72fZ+0+viqdW++Q/320h1naL/0f75Of7ruxtqd8VWq3rjyRuPL3+QOjZeGxk9C48Whcf/O2vHtlWnUJPWhc3e0siqNmnrfbDb1x2OXplVpVJt6TWh2j+yrSKP2tEwUkb8PD9eKNKrN2tp9VRu1zraxWd1GfdNYq2qjZGzmqNer0mioeyW3z+vIpnp471I2Gje6uz+uev7koesPR64Cytho3us1fX+S0kP3qfF4sDVXwsY8sXYYqUmmefyCtXyN5s1qDT2c5Cmla1xN8YPI4o6yNW6meC5SM+qFS/OSNb5P8Uykcd+82Y0sV+PuFE9Faka+7HZ3IkvVuJ94NNJsLJfdncgyNRYP1BORxvq3LP39cC1R4+EUDyM1o7FddreR5Wk8nrgXaeyMenu4lqbxVGIh0iguu+vIsjSeThQ2kWZjb9ld/XWnJI3Hlpv9SWrm08Gyuzwny9F4doqbSKNxZNm9kUrS+FFiHnlrHB21OCe1EjRqHycK7qllt1uCOZqfSqzpJ5fd7tU3/jI+lXiu/ql27Y1fTcxdeePXA9F4eWhEIxqvhyae27/BVf9PoFT/Fo+f/XPzRWjf49IZAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA/1uapFafpFSfJFcfGqvhPzcS8hMP40etG4m1ui3Zm52kpbRI6z2qJTYNh6WLXDWSt8QiUdJqy7LXbrEX2WoT2bPJ2B1HHY94PZFv9xRXaWdOWrrI9RztwLJ87lDP5kHKAiUN+nKYUqq8jtzfL+HEb/eSYGAoScrn0V9/kETe/bESb7u5sNXzLPFiizf5XTyyvd+mMeE9mvoe7TOFUCt1FLqYRNychIr7Gt75qv88TrI79tDOsr8+RtL3+rJFxIMXN2KFob18P52JGLGJiDJvMJr7PA1T7qdv6aDPyTwb+qTQSEaszSacUyaTNn157hE5cNp2xozEtYJY4WO3Zyu3UusSx6nlhGw0n6bhJHN4n485Xcz9gRMmodV3Bs6Ip37IucOGzA/8fkDr04C9+gFlvrfbKPdoEgWLUZQs8vORTnsL5thEUfyxa8XPSjJxZ4pixJPWBRrFANJxJ45nNBXnUcgZ85OY+QuVK5O3IHwOWMKcwRu1mcLjWTKM43jOWZ+Fc6vQaCVjJaSxMxC3BV3QmGZ/WrHbcd8SNet01CxV3b6puIl9+rH8mF4wTP6ENI05jVNCqcMp5XSeJouYzmmS8JHjTBkLX0PqOJlPQ0adwE+CPik0yiQiXjbLyHQWeZGVjQgZyC/RjFh25FkzccwPZnLkydFFFlVx1k1tIk69/FWsLrI4BfNVZWrJ4mX5lE3kfKmZ2vkTuNhLplOLFM9HebVKLW/LVWy1e/3VB9vLBddy1YDGavgXIXiOOOUMCCAAAAAASUVORK5CYII=",
                    New=99,
                };

                db.Products.Add(selectedItem);
                ProductCreated.Add(selectedItem);
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
        public async void RemoveCommandExec(object o)
        {
            
            using (var db = new GoninDigitalDBContext())
            {
                try
                {
                    if (Products.Contains(selectedItem))
                    {
                        SelectedItem.StatusId = (int)Constants.ProductStatus.REMOVED;
                        db.Update(SelectedItem);
                        Products.Remove(SelectedItem);
                    }
                    if(ProductCreated.Contains(selectedItem))
                    {
                        SelectedItem.StatusId = (int)Constants.ProductStatus.REMOVED;
                        db.Update(SelectedItem);
                        ProductCreated.Remove(SelectedItem);
                    }
                    
                    _ = db.SaveChanges();
                    var content = new ContentDialog
                    {
                        Content = "A Product have been removed!",
                        Title = "Notification",
                        PrimaryButtonText = "Ok",

                    };
                    await content.ShowAsync();
                    
                }
                catch (Exception e)
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Warning",
                        Content = e.ToString(),
                        PrimaryButtonText = "Ok"
                    };
                     await dialog.ShowAsync();
                    
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
                db.ProductSpecDetails.RemoveRange(db.ProductSpecDetails.Where(o => o.ProductId==selectedItem.Id));
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
                        db.ProductSpecDetails.RemoveRange(db.ProductSpecDetails.Where(o => o.ProductId == selectedItem.Id));
                        SelectedProductSpecs.RemoveAll(o => string.IsNullOrEmpty(o.Value));
                        SelectedProductSpecs.ForEach(o =>
                        {
                            o.Spec = null;
                        });
                        db.ProductSpecDetails.AddRange(SelectedProductSpecs);
                        db.Entry(SelectedItem).State = EntityState.Modified;
                        db.SaveChanges();
                        
                    }
                    catch
                    {
                        var dialog = new ContentDialog
                        {
                            Title = "Warning",
                            Content = "An unexpected error occured!",
                            PrimaryButtonText = "Ok"
                        };
                        dialog .ShowAsync();
                     
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
