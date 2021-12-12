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
    class ManageShopPageViewModel : BaseViewModel
    {
        #region Properties
        private ObservableCollection<Vendor> l_Shop;
        public ObservableCollection<Vendor> L_Shop { get { return l_Shop; } set { l_Shop = value; OnPropertyChanged(); } }
        private ObservableCollection<Vendor> l_ShopNew;
        public ObservableCollection<Vendor> L_ShopNew { get { return l_ShopNew; } set { l_ShopNew = value; OnPropertyChanged(); } }
        private ObservableCollection<Vendor> l_ShopClosed;
        public ObservableCollection<Vendor> L_ShopClosed { get { return l_ShopClosed; } set { l_ShopClosed = value; OnPropertyChanged(); } }
        private Vendor _SelectedItem;
        public Vendor SelectedItem { get { return _SelectedItem; } set { _SelectedItem = value; OnPropertyChanged(); } }
        private IEnumerable<Vendor> selectedVendors;
        public IEnumerable<Vendor> SelectedVendors
        {
            get { return selectedVendors; }
            set { selectedVendors = value; OnPropertyChanged(); }
        }
        private string searchName;
        public string SearchName
        {
            get { return searchName; }
            set 
            { 
                searchName = value; OnPropertyChanged();
            }
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
            SelectedVendors = new ObservableCollection<Vendor>();
            using (var db = new GoninDigitalDBContext())
            {
                L_Shop = new ObservableCollection<Vendor>(db.Vendors.Include(x => x.Owner).Where(x => x.ApprovalStatus == (byte)Utils.Constants.ApprovalStatus.ACTIVE));
                L_ShopNew = new ObservableCollection<Vendor>(db.Vendors.Include(x => x.Owner).Where(x => x.ApprovalStatus == (byte)Utils.Constants.ApprovalStatus.REQUEST));
            }
            L_ShopClosed = new ObservableCollection<Vendor>();
            RemoveCommand = new RelayCommand<Vendor>(o => true,
               vendor => { RemoveExec(vendor); });
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
            SearchName = "";
        }
        #endregion
        #region Private Methods
        private void RemoveExec(Vendor vendor)
        {
            L_ShopNew.Remove(vendor);
            using (var db = new GoninDigitalDBContext())
            {
                db.Vendors.Remove(vendor);
                db.Users.First(x => x.Id == vendor.OwnerId).TypeId = (int)Utils.Constants.UserType.CUSTOMER;
                db.SaveChanges();
            }
        }
        private void AcceptExec(Vendor vendor)
        {
            L_ShopNew.Remove(vendor);
            L_Shop.Add(vendor);
            using (var db = new GoninDigitalDBContext())
            {
                db.Vendors.First(x => x.Id == vendor.Id).ApprovalStatus = 1;
                db.Users.First(x => x.Id == vendor.OwnerId).TypeId = (int)Utils.Constants.UserType.VENDOR;
                db.SaveChanges();
            }
        }
        private void RemoveSelectionsExec(IEnumerable<Vendor> selectedVendors)
        {

            using (var db = new GoninDigitalDBContext())
            {
                foreach (Vendor vendor in selectedVendors.ToList())
                {
                    L_ShopNew.Remove(vendor);
                    db.Vendors.Remove(vendor);
                    db.Users.First(x => x.Id == vendor.OwnerId).TypeId = (int)Utils.Constants.UserType.CUSTOMER;
                    db.SaveChanges();
                }
            }
        }
        private void AcceptSelectionsExec(IEnumerable<Vendor> selectedVendors)
        {
            if (selectedVendors != null)
            {
                using (var db = new GoninDigitalDBContext())
                {
                    foreach (var vendor in selectedVendors.ToList())
                    {
                        L_ShopNew.Remove(vendor);
                        L_Shop.Add(vendor);
                        db.Vendors.First(x => x.Id == vendor.Id).ApprovalStatus = 1;
                        db.Users.First(x => x.Id == vendor.OwnerId).TypeId = (int)Utils.Constants.UserType.VENDOR;
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
                foreach(Vendor v in L_Shop)
                {
                    if(vendor.Id==v.Id)
                    {
                        L_Shop.Remove(v);
                        break;
                    }
                }
                db.Vendors.First(x => x.Id == vendor.Id).ApprovalStatus = (byte)Utils.Constants.ApprovalStatus.CLOSED;
                db.Users.First(x => x.Id == vendor.OwnerId).TypeId = (int)Utils.Constants.UserType.CUSTOMER;
                db.SaveChanges();
            }
        }
        public void SearchVendor()
        {
            string s = SearchName.ToLower();
            if(SearchName!="")
            {
                using (var db = new GoninDigitalDBContext())
                {
                    L_Shop = new ObservableCollection<Vendor>(db.Vendors.Include(x => x.Owner).Where(x => x.ApprovalStatus == (byte)Utils.Constants.ApprovalStatus.ACTIVE));
                }
                int count = 0;
                while(count<L_Shop.Count())
                {
                    if (!L_Shop[count].Name.ToLower().Contains(s))
                        L_Shop.RemoveAt(count);
                    else
                        count += 1;
                }
            }
        }
        public void SearchChanged()
        {
            if (SearchName=="")
            {
                using (var db= new GoninDigitalDBContext())
                {
                    L_Shop = new ObservableCollection<Vendor>(db.Vendors.Include(x => x.Owner).Where(x => x.ApprovalStatus == (byte)Utils.Constants.ApprovalStatus.ACTIVE));
                }
            }
        }
        public void ToggleChanged(bool flag)
        {
            using (var db = new GoninDigitalDBContext())
            {
                if (flag)
                { 
                    L_Shop = new ObservableCollection<Vendor>(db.Vendors.Include(x => x.Owner).Where(x => x.ApprovalStatus != (byte)Utils.Constants.ApprovalStatus.REQUEST)); 
                }
                else
                    L_Shop = new ObservableCollection<Vendor>(db.Vendors.Include(x => x.Owner).Where(x => x.ApprovalStatus == (byte)Utils.Constants.ApprovalStatus.ACTIVE));
            }
        }
        public void StrikeThrough()
        {
            L_ShopClosed = new ObservableCollection<Vendor>();
            foreach(Vendor vendor in L_Shop)
            {
                if (vendor.ApprovalStatus == 2)
                    L_ShopClosed.Add(vendor);
            }
        }
        #endregion
    }
}
