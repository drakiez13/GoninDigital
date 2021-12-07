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
        private bool hasVendor;
        public bool HasVendor
        {

            get { return hasVendor; }
            set { hasVendor = value;OnPropertyChanged(); }
        }
        private string visibilityOwner;
        public string VisibilityOwner
        {

            get { return visibilityOwner; }
            set { visibilityOwner = value; OnPropertyChanged(); }
        }
        private bool isOwner;
        public bool IsOwner
        {
            get { return isOwner; }
            set { isOwner = value; OnPropertyChanged(); }
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
        private string newVendorName = null;
        public string NewVendorName
        {
            get { return newVendorName; }
            set { newVendorName = value; OnPropertyChanged(); }
        }
        private string vendorName = null;
        public string VendorName
        {
            get { return vendorName; }
            set { vendorName = value; OnPropertyChanged(); }
        }

        public int OnPrimaryButtonClick { get; private set; }
        public int PrimaryButtonClick { get; private set; }


        public ICommand EditAvatarCommand { get; set; }
        public ICommand EditCoverPhotoCommand { get; set; }

        private void InitVendor()
        {
            using (var db = new GoninDigitalDBContext())
            {
                try
                {
                    
                    Vendor = db.Vendors.Include(o => o.Owner)
                        .Include(o => o.Products)
                        .First(o => o.Owner.UserName == Settings.Default.usrname );
                    db.ProductCategories.ToList();
                    Products = new ObservableCollection<Product>(Vendor.Products.Where(o=>o.StatusId==(int)Constants.ProductStatus.ACCEPTED).ToList());
                    HasVendor = true;
                    VendorName = Vendor.Name;
                    VisibilityOwner = "Visible";
                    
                }
                catch
                {
                   
                    HasVendor = false;
                }
            }
        }
        
        public ICommand EditCommand { get; set; }
        public void EditCommandExec(object o)
        {
            
            var dialog = new ContentDialog
            {
                Content = new EditProductDialog(),

                Title = "Edit Product",
                PrimaryButtonText = "Change",
                CloseButtonText = "Cancel",

                PrimaryButtonCommand = new RelayCommand<object>((p) => true, (p) => { EditBtnExec(); }),
            };
            dialog.ShowAsync();
        }
        public ICommand UpgradeCommand { get; set; }
        public void UpgradeCommandExec()
        {
            var dialog = new ContentDialog
            {
                Content = new UpgradeVendorDialog(),

                Title = "Upgrade",
                PrimaryButtonText = "Upgrade",
                CloseButtonText = "Cancel",

                PrimaryButtonCommand = new RelayCommand<object>((p) => true, (p) => { UpgradeExec(); }),
            };
            dialog.ShowAsync();
        }
        public ICommand RemoveCommand { get; set; }
        public void RemoveCommandExec(object o)
        {
            using (var db = new GoninDigitalDBContext())
            {
                try
                {
                    SelectedItem.StatusId = (int)Constants.ProductStatus.REMOVED;
                    db.Update(SelectedItem);
                    _=db.SaveChanges();
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
        public void OnNavigatedTo()
        {
            if(isOwner)
            {
                
                Thread thread = new Thread(InitVendor);
                thread.Start();
            }
            else
            {
                
                HasVendor = true;
            }
            
        }
        public ICommand ImageEditCommand { get; set; }
        public async void ImageEditCommandExec(object o)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Choose Image..";

            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg|All files (*.*)|*.*";
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
        public MyShopViewModel()
        {
            
            EditCommand = new RelayCommand<Product>(o => true, o => EditCommandExec(o));
            RemoveCommand = new RelayCommand<Product>(o => true, o => RemoveCommandExec(o));
            ImageEditCommand = new RelayCommand<Product>(o => true, o => ImageEditCommandExec(o));
            UpgradeCommand = new RelayCommand<object>((p) => true, (p) => { UpgradeCommandExec(); });
            EditCoverPhotoCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { EditCoverPhotoExec(); });
            EditAvatarCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { EditAvatarExec(); });
            ResetVendorInfoCommand = new RelayCommand<object>((p) => true, (p) => { ResetVendorInfoExec(); });
            SaveVendorInfoCommand = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(VendorName))
                {
                    return false;
                }
                
                return true;
            }, (p) => { SaveVendorConfirm(); });
        }
        public async void EditAvatarExec()
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
        public void EditBtnExec()
        {
            
            using (var db = new GoninDigitalDBContext())
            {
                db.Products.Update(selectedItem);
                db.SaveChanges();
            }
            MessageBox.Show("edited");
        }
        public void UpgradeExec()
        {
            
            using (var db = new GoninDigitalDBContext())
            {
                int userId = db.Users.First(u => u.UserName == Settings.Default.usrname).Id;
                Vendor newVendor = new Vendor() { Name = NewVendorName, OwnerId = userId, ApprovalStatus=0};
                db.Vendors.Add(newVendor);
                Vendor = newVendor;
                HasVendor = true;
                db.SaveChanges();
            }
        }
    }
}
