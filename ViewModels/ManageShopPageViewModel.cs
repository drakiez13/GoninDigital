using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GoninDigital.Models;
using GoninDigital.Views;
using GoninDigital.Views.DashBoardPages;

using GoninDigital.Properties;
using GoninDigital.Views.SharedPages;
using Microsoft.EntityFrameworkCore;
using ModernWpf.Controls;

namespace GoninDigital.ViewModels
{
    class ManageShopPageViewModel:BaseViewModel
    {
        #region Properties
        private ObservableCollection<Vendor> l_Shop;
        public ObservableCollection<Vendor> L_Shop { get { return l_Shop; } set { l_Shop = value; OnPropertyChanged(); } }
        private ObservableCollection<Vendor> l_ShopNew;
        public ObservableCollection<Vendor> L_ShopNew { get { return l_ShopNew; } set { l_ShopNew= value; OnPropertyChanged(); } }
        private Vendor _SelectedItem;
        public Vendor SelectedItem { get { return _SelectedItem; } set { _SelectedItem = value; OnPropertyChanged(); } }
        private IEnumerable<Vendor> selectedVendors;
        public IEnumerable<Vendor> SelectedVendors
        {
            get { return selectedVendors; }
            set { selectedVendors = value; OnPropertyChanged(); }
        }
        public ICommand DeleteCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand ShowVendorCommand { get; set; }
        public ICommand AcceptCommand { get; set; }
        public ICommand RemoveSelectionsCommand { get; set; }
        public ICommand AcceptSelectionsCommand { get; set; }
        #endregion
        #region Constructor
        public ManageShopPageViewModel()
        {
            SelectedVendors=new ObservableCollection<Vendor>();
            using (var db = new GoninDigitalDBContext())
            {
                L_Shop = new ObservableCollection<Vendor>(db.Vendors.Where(x => x.ApprovalStatus == 1));
                L_ShopNew = new ObservableCollection<Vendor>(db.Vendors.Where(x => x.ApprovalStatus == 0));
            }
            RemoveCommand = new RelayCommand<Vendor>(o => true,
               vendor => { RemoveExec(vendor); });
            ShowVendorCommand = new RelayCommand<Vendor>(o => true,
                vendor => DashBoard.RootFrame.Navigate(new ShopPage(vendor.Id)));
            AcceptCommand = new RelayCommand<Vendor>(o => true, vendor => { AcceptExec(vendor); });
            RemoveSelectionsCommand = new RelayCommand<Vendor>(o => true, SelectedVendors =>
            { RemoveSelectionsExec(selectedVendors); });
            AcceptSelectionsCommand = new RelayCommand<Vendor>(o => true, SelectedVendors =>
            { AcceptSelectionsExec(selectedVendors); });
            DeleteCommand = new RelayCommand<Object>((p) =>
            {
                if (SelectedItem != null)
                {
                    return true;
                }
                return false;
            }, (p) =>
            {
                DeleteExec();
            });
        }
        #endregion
        #region Private Methods
        private void RemoveExec(Vendor vendor)
        {
            L_ShopNew.Remove(vendor);
            using (var db = new GoninDigitalDBContext())
            {
                db.Vendors.First(x => x.Id == vendor.Id).ApprovalStatus = 2;
                db.SaveChanges();
            }
        }
        private void AcceptExec(Vendor vendor)
        {
            using (var db = new GoninDigitalDBContext())
            {
                db.Vendors.First(x => x.Id == vendor.Id).ApprovalStatus = 1;
                L_ShopNew.Remove(vendor);
                L_Shop.Add(vendor);
                db.SaveChanges();
            }
        }
        private void RemoveSelectionsExec(IEnumerable<Vendor> selectedVendors)
        {
            
            using (var db = new GoninDigitalDBContext())
            {
                foreach(Vendor vendor in selectedVendors.ToList())
                {
                    L_ShopNew.Remove(vendor);
                    db.Vendors.First(x => x.Id == vendor.Id).ApprovalStatus = 2;
                }
                db.SaveChanges();
            }
        }
        private void AcceptSelectionsExec(IEnumerable<Vendor> selectedVendors)
        {
            if (selectedVendors!=null)
            {
                using (var db = new GoninDigitalDBContext())
                {
                    foreach (var vendor in selectedVendors.ToList())
                    {
                        db.Vendors.First(x => x.Id == vendor.Id).ApprovalStatus = 1;
                        L_ShopNew.Remove(vendor);
                        L_Shop.Add(vendor);
                        db.SaveChanges();
                    }
                }
            }
        }
        private void DeleteExec()
        {
            using (var db = new GoninDigitalDBContext())
            {
                var vendor = db.Vendors.First(x => x.Id == SelectedItem.Id);
                L_Shop.Remove(vendor);
                db.Vendors.First(x => x.Id == vendor.Id).ApprovalStatus = 2;
                db.SaveChanges();
            }
        }
#endregion
    }
}
