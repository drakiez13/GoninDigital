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

        public int OnPrimaryButtonClick { get; private set; }
        public int PrimaryButtonClick { get; private set; }

        private void InitVendor()
        {
            using (var db = new GoninDigitalDBContext())
            {
                try
                {
                    Vendor = db.Vendors.Include(o => o.Owner)
                        .Include(o => o.Products)
                        .First(o => o.Owner.UserName == Settings.Default.usrname);
                    /*Products = db.Products.Where(o => o.VendorId == Vendor.Id).ToList();*/
                    Products = new ObservableCollection<Product>(Vendor.Products.ToList());
                    HasVendor = true;
                }
                catch
                {

                    HasVendor = false;
                }
            }
        }
        
        public ICommand EditCommand { get; set; }
        public void EditCommandExec(Product product)
        {
            SelectedItem = product;
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
        public ICommand RemoveCommand { get; set; }
        public async void RemoveCommandExec(Product product)
        {
            using (var db = new GoninDigitalDBContext())
            {
                try
                {
                    db.Products.Remove(product);
                    
                    await db.SaveChangesAsync();

                    Products.Remove(product);
                    MessageBox.Show("removed");
                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message);
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
        public MyShopViewModel()
        {
            EditCommand = new RelayCommand<Product>(o => true, o => EditCommandExec(o));
            RemoveCommand = new RelayCommand<Product>(o => true, o => RemoveCommandExec(o));

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
    }
}
